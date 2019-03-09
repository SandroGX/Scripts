using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "Anim", menuName = "Motor/Anim", order = 0)]
    public class AnimState : MotorState
    {
        public Anim[] conditions;


        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            PlayConditions(motor);
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
