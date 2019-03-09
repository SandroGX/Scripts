using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "CCJump", menuName = "Motor/CharacterController/Jump", order = 2)]
    public class CCJump : CCAccelaration
    {

        public float jumpHeight = 4;
        public int staminaVarInst;
       
        public override void ProcessMovement(Motor motor)
        {
            float jumpVel = Mathf.Sqrt(-2 * -motor.gravity.magnitude * jumpHeight);
            motor.fallVelocity = -motor.gravity.normalized * jumpVel * Time.fixedDeltaTime;

            base.ProcessMovement(motor);  
        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            motor.movementVelocity += motor.platformVelocity;
            motor.platformVelocity = motor.fallVelocity = motor.movementAngVelocity = motor.platformAngVelocity = Vector3.zero; 
            motor.character.stamina.Add(-staminaVarInst);
        }
    }
}
