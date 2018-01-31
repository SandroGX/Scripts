using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "Salto", menuName = "Motor/CharacterController/Salto", order = 2)]
    public class CCSalto : CCAccelaration
    {

        public float saltoAltura = 4;
        public int staminaVarInst;
       
        public override void ProcessMovement(Motor motor)
        {
            float saltoVelocidade = Mathf.Sqrt(-2 * -motor.gravity.magnitude * saltoAltura);
            motor.fallVelocity = -motor.gravity.normalized * saltoVelocidade * Time.fixedDeltaTime;

            base.ProcessMovement(motor);  
        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            motor.movementVelocity += motor.platformVelocity;
            motor.platformVelocity = motor.fallVelocity = motor.movementAngVelocity = motor.platformAngVelocity = Vector3.zero; 
            motor.character.stamina.Add(-staminaVarInst);
        }


        public override bool CanStay(Motor motor)
        {
            return motor.isGrounded && !motor.character.tired && base.CanStay(motor); //&& canJump
        }
    }
}
