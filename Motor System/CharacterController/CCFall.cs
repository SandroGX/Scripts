using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.MotorSystem
{
    [CreateAssetMenu(fileName = "CCFall", menuName = "Motor/CharacterController/Fall", order = 1)]
    public class CCFall : BasicMovement
    {

        public float terminalVelocity = 15;

        public override void ProcessMovement(Motor motor)
        {
            base.ProcessMovement(motor);

            motor.velocity = MotorUtil.MovUniVarDir(motor.velocity, motor.gravity.normalized * terminalVelocity, 1, terminalVelocity, 0, motor.gravity.magnitude);
        }
    }
}
