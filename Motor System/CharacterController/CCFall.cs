using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "CCFall", menuName = "Motor/CharacterController/Fall", order = 1)]
    public class CCFall : BasicMovement
    {

        public float terminalVelocity = 15;

        public override void ProcessMovement(Motor motor)
        {
            base.ProcessMovement(motor);

            motor.fallVelocity = MotorUtil.MovUniVar(motor.fallVelocity, motor.gravity.normalized * terminalVelocity, 1, terminalVelocity, 0, motor.gravity.magnitude, Time.fixedDeltaTime);
        }

        public override void Construct(Motor motor)
        {
            base.Construct(motor);

            motor.movementVelocity += motor.platformVelocity;
            motor.platformVelocity = motor.movementAngVelocity = motor.platformAngVelocity = Vector3.zero;
        }
    }
}
