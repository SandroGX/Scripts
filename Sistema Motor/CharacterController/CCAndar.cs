using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "Andar", menuName = "Motor/CharacterController/Andar", order = 0)]
    public class CCAndar : MovimentoBasico
    {
        public int staminaVar;


        public override void ProcessarMovimento(Motor motor)
        {
            base.ProcessarMovimento(motor);

            CCMotor cMotor = (CCMotor)motor;


            //motor.transform.up = -cMotor.gravidadeDirecao.normalized;

            cMotor.velocidade = Vector3.ProjectOnPlane(motor.velocidade, cMotor.ChaoNormal).normalized * motor.velocidade.magnitude;

            cMotor.OlharDir = Vector3.ProjectOnPlane(cMotor.Alvo - cMotor.transform.position, cMotor.ChaoNormal).normalized;

            float angulo = (motor.input != Vector3.zero) ? MotorUtil.GetAnguloComSinal(motor.transform.forward, cMotor.OlharDir) : 0;

            motor.velocidadeRotacional.y = MotorUtil.MovUniVar(motor.velocidadeRotacional.y, angulo, velAngMin * Time.fixedDeltaTime,
                    velAngMax * Time.fixedDeltaTime, acelAngMin * Time.fixedDeltaTime * Time.fixedDeltaTime,
                    acelAngMax * Time.fixedDeltaTime * Time.fixedDeltaTime);
        }

        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            motor.character.staminaVar -= staminaVar;
        }


        public override void Deconstruct(Motor motor)
        {
            base.Deconstruct(motor);

            motor.character.staminaVar += staminaVar;
        }


        public override void Transicao(Motor motor)
        {
            if (motor.character.stamina < staminaVar && motor.defaultEstado)
            {
                motor.DefaultEstado();
                return;
            }

            if (!motor.NoChao())
            {
                motor.MudarEstado(((CCMotor)motor).queda);
                return;
            }
            else
            {
                motor.ProximoEstado();
                return;
            }
        }
    }
}
