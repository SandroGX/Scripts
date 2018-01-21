using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "Queda", menuName = "Motor/CharacterController/Queda", order = 1)]
    public class CCQueda : MovimentoBasico
    {

        public float velocidadeTerminal = 15;

        public override void ProcessMovement(Motor motor)
        {
            CCMotor cMotor = (CCMotor)motor;

            base.ProcessMovement(motor);

            motor.fallVelocity = MotorUtil.MovUniVar(motor.fallVelocity, cMotor.gravity.normalized * velocidadeTerminal, 1, velocidadeTerminal, 0, cMotor.gravity.magnitude, Time.fixedDeltaTime);

        }

        public override void Construct(Motor motor)
        {
            base.Construct(motor);

            motor.movementVelocity += motor.platformVelocity;
            motor.platformVelocity = motor.movementAngVelocity = motor.platformAngVelocity = Vector3.zero;
        }


        public override bool CanStay(Motor motor)
        {
            return !((CCMotor)motor).isGrounded;
        }
        
    }
}
