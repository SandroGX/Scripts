using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.MotorSystem
{
    [CreateAssetMenu(fileName = "Anim", menuName = "Motor/Anim", order = 0)]
    public class AnimState : MotorState
    {
        public Anim[] conditions;


        public override void OnStateEnter(Motor motor)
        {
            PlayConditions(motor);
        }

        public override void OnStateExit(Motor motor)
        {
            throw new System.NotImplementedException();
        }

        public override void ProcessMovement(Motor motor)
        {
            
        }


        protected virtual void PlayConditions(Motor motor)
        {
            foreach (Anim condicao in conditions) condicao.SetParam(motor.anim);
        }

    }
}
