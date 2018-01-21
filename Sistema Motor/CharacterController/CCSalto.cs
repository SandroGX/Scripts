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
       
        public override void ProcessMovement(Motor motor)
        {
            CCMotor cMotor = (CCMotor)motor;

            float saltoVelocidade = Mathf.Sqrt(-2 * -cMotor.gravity.magnitude * saltoAltura);

            motor.fallVelocity = -cMotor.gravity.normalized * saltoVelocidade * Time.fixedDeltaTime;

            motor.movementVelocity += Vector3.ProjectOnPlane(cMotor.input, cMotor.floorHit.normal) * aceleracaoXZ * Time.fixedDeltaTime;
               
        }


        public override void Construct(Motor motor)
        {
            motor.movementVelocity += motor.platformVelocity;
            motor.platformVelocity = motor.fallVelocity = motor.movementAngVelocity = motor.platformAngVelocity = Vector3.zero; 
            motor.character.stamina.Add(-staminaVarInst);
        }


        public override bool CanStay(Motor motor)
        {
            return ((CCMotor)motor).isGrounded && (motor.character.stamina.value >= staminaVarInst || motor.currentState == this); //&& canJump
        }
    }
}
