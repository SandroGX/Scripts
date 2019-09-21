using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.MotorSystem
{
    [CreateAssetMenu(fileName = "CCJump", menuName = "Motor/CharacterController/Jump", order = 2)]
    public class CCJump : CCAccelaration
    {

        public float jumpHeight = 4;
       
        public override void ProcessMovement(Motor motor)
        {
            float jumpVel = Mathf.Sqrt(-2 * -motor.gravity.magnitude * jumpHeight);
            motor.velocity += -motor.gravity.normalized * jumpVel * Time.fixedDeltaTime;

            base.ProcessMovement(motor);  
        }


        public override void OnStateEnter(Motor motor)
        {
            base.OnStateEnter(motor);
        }
    }
}
