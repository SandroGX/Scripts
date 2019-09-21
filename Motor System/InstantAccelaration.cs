using UnityEngine;
using System.Collections;
using GX.MotorSystem;

namespace GX.MotorSystem
{
    [CreateAssetMenu(fileName = "Instant Accelaration", menuName = "Motor/Instant Accelaration", order = 1)]
    public class InstantAccelaration : MotorState
    {
        public float accelaration = 5;

        public override void OnStateEnter(Motor motor)
        {
            motor.velocity += motor.input * accelaration;
        }

        public override void OnStateExit(Motor motor)
        {
            throw new System.NotImplementedException();
        }

        public override void ProcessMovement(Motor motor)
        {
            throw new System.NotImplementedException();
        }
    }
}
