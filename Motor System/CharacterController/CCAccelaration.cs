using UnityEngine;
using System.Collections;

namespace GX.MotorSystem
{
    [CreateAssetMenu(fileName = "CCAccelaration", menuName = "Motor/CharacterController/CCAccelaration", order = 2)]
    public class CCAccelaration : InstantAccelaration
    {

        public override void ProcessMovement(Motor motor)
        {
            MotorUtil.MotorInputOnSurface(motor);
            base.ProcessMovement(motor);
        }
    }
}
