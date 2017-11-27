using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.SistemaMotor
{
    public class CCMotor : Motor
    {
        [HideInInspector]
        public CharacterController controller;

        RaycastHit chaoHit;

        public Vector3 ChaoNormal { get; set; }
        public Vector3 OlharDir { get; set; }
        public Vector3 Alvo { get; set; }

        public float Gravidade = 9.8f;
        public Vector3 gravidadeDirecao = -Vector3.up;
        public MotorEstado queda;

        protected override void Awake()
        {
            base.Awake();
            controller = GetComponent<CharacterController>();
        }


        protected override void MotorUpdate()
        {
            base.MotorUpdate();

            anim.SetFloat("Velocidade", velocidade.magnitude / Time.fixedDeltaTime);
            anim.SetFloat("Velocidade Angular", velocidadeRotacional.y / Time.fixedDeltaTime);

            controller.Move(velocidade);
            transform.eulerAngles += velocidadeRotacional;
        }


        public override bool NoChao()
        {
            Physics.Raycast(transform.position, gravidadeDirecao, out chaoHit);
            
            ChaoNormal = chaoHit.normal;

            if (chaoHit.distance > 0.2f)
                return false;
            else return true;
           
        }


        public void PorNoChao()
        {
            transform.position = chaoHit.point + (-gravidadeDirecao.normalized) * 0.1f;
        }

    }
}
