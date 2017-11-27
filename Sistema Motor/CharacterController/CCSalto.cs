using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "Salto", menuName = "Motor/CharacterController/Salto", order = 2)]
    public class CCSalto : MotorEstado
    {

        public float saltoAltura = 4;
        public float aceleracaoXZ = 2;
        public int staminaVarInst;
       
        public override void ProcessarMovimento(Motor motor)
        {
            CCMotor cMotor = (CCMotor)motor;

            float saltoVelocidade = Mathf.Sqrt(-2 * -cMotor.Gravidade * saltoAltura);

            Vector3 salto = -cMotor.gravidadeDirecao * saltoVelocidade;

            Vector3 impulso = Vector3.ProjectOnPlane(cMotor.input, cMotor.ChaoNormal) * aceleracaoXZ;

            cMotor.velocidade = cMotor.velocidade + (salto + impulso) * Time.fixedDeltaTime;
               
        }


        public override void ModificarCharacter(Motor motor)
        {
            base.ModificarCharacter(motor);

            motor.character.stamina -= staminaVarInst;
        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            ModificarCharacter(motor);
        }


        public override void Transicao(Motor motor)
        {
            if (motor.character.stamina < staminaVarInst)
            {
                motor.DefaultEstado();
                return;
            }

            if (!motor.NoChao())
            {
                motor.MudarEstado(((CCMotor)motor).queda);
                return;
            }

        }
    }
}
