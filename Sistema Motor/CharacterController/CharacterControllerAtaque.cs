using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "Ataque", menuName = "Motor/CharacterController/Ataque", order = 3)]
    public class CharacterControllerAtaque : MotorEstado
    {

        bool chegou = false;
        bool atacou = false;

        [HideInInspector]
        public Vector3 alvo;
        public Anim aproximar;
        public Anim ataque;
        public Anim cancelar;

        public float velocidade;
        public float velocidadeAngularMax = 360;
        public float minDistanciaDeAtaque = 1.3f;
        public float minDistanciaDeAproximacao = 3;
        public float minTempo = 2;

        public int staminaVarInst;

        float tempo;
        Vector3 direcao;

        public override void ProcessarMovimento(Motor motor)
        {
            CCMotor cMotor = (CCMotor)motor;

            RaycastHit hit;

            if (Physics.Linecast(cMotor.transform.position, cMotor.Alvo, out hit))
            {
                alvo = hit.point;
            }
            else alvo = cMotor.Alvo;

            direcao = alvo - cMotor.transform.position;

            direcao.y = 0;

            direcao = direcao.normalized;

            float dis = Vector3.Distance(cMotor.transform.position, alvo);

            //minTempo = dis - minDistanciaDeAtaque / velocidade;

            if (dis > minDistanciaDeAtaque && dis < minDistanciaDeAproximacao && tempo <= minTempo && !chegou)
            {
                tempo += Time.deltaTime;
                cMotor.velocidade = direcao * velocidade;
            }
            else
            {
                chegou = true;
                cMotor.velocidade = Vector3.zero;
            }

            float velAng = velocidadeAngularMax * Time.deltaTime;
            float angulo = Vector3.Angle(cMotor.OlharDir, direcao); ;

            angulo = (angulo <= velAng) ? angulo : velAng;

            
            angulo = angulo * Mathf.Sign(Vector3.Dot(direcao, Quaternion.AngleAxis(90, Vector3.up) * cMotor.OlharDir));
            

            cMotor.velocidadeRotacional = Vector3.up * angulo;

            if (chegou && !atacou)
            {
                ataque.SetParam(cMotor.anim);
                atacou = true;
            }
            else aproximar.SetParam(cMotor.anim);
        }



        public override void ModificarCharacter(Motor motor)
        {
            base.ModificarCharacter(motor);

            ((CCMotor)motor).character.stamina -= staminaVarInst;
        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);
        }


        public override void Deconstruct(Motor motor)
        {
            base.Deconstruct(motor);

            CCMotor cMotor = (CCMotor)motor;

            cancelar.SetParam(cMotor.anim);

        }


        public override void Transicao(Motor motor)
        {
            base.Transicao(motor);

            CCMotor cMotor = (CCMotor)motor; 

            if(!chegou && cMotor.character.stamina < staminaVarInst)
            {
                cMotor.DefaultEstado();
                return;
            }


            if (cMotor.animAcabou)
            {
                if(cMotor.character.stamina < staminaVarInst)
                {
                    cMotor.DefaultEstado();
                    return;
                }

                cMotor.ProximoEstado();
            }
        }

    }
}