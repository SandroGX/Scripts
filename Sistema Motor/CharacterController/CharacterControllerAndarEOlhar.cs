using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "OlharEAndar", menuName = "Motor/CharacterController/Olhar e Andar", order = 4)]
    public class CharacterControllerAndarEOlhar :  MotorEstado
    {

        public float velocidadeAngularMax;
        public float velocidadeMax;

        float angulo;

        public override void ProcessarMovimento(Motor motor)
        {
            CCMotor cMotor = (CCMotor)motor;

            cMotor.velocidade = cMotor.input * velocidadeMax * Time.fixedDeltaTime;

            float vAMax = velocidadeAngularMax * Time.fixedDeltaTime;

            Vector3 dir = cMotor.Alvo - cMotor.transform.position;

            dir.y = 0;

            dir = dir.normalized;

            angulo = Vector3.Angle(cMotor.OlharDir, dir);

            float sinal = Mathf.Sign(Vector3.Dot(dir, Quaternion.AngleAxis(90, Vector3.up) * cMotor.OlharDir));

            if (angulo > vAMax)
                cMotor.velocidadeRotacional = new Vector3(0, vAMax * sinal, 0);
            else cMotor.velocidadeRotacional = new Vector3(0, angulo * sinal, 0);
        }


        public override void Construct(Motor motor)
        {
            MotorUtil.NavAgent(motor, null);
 
            base.Construct(motor);
        }


        public override void Transicao(Motor motor)
        {
            if (!motor.NoChao())
                motor.MudarEstado(((CCMotor)motor).queda);
            else motor.ProximoEstado();
        }

    }
}
