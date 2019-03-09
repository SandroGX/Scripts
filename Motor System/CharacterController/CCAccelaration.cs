using UnityEngine;
using System.Collections;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "CCAccelaration", menuName = "Motor/CharacterController/CCAccelaration", order = 2)]
    public class CCAccelaration : InstantAccelaration
    {

        public override void ProcessMovement(Motor motor)
        {
            motor.InputOnSurface();
            base.ProcessMovement(motor);
        }
    }
}
