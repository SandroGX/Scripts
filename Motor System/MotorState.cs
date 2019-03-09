using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.MotorSystem {
    public abstract class MotorState : ScriptableObject
    {
        public bool animExit;
        public Anim[] animConditions;


        [SerializeField]
        public List<MotorState> passiveStates = new List<MotorState>();

        public abstract void ProcessMovement(Motor motor);


        public virtual void Construct(Motor motor)
        {
            foreach (Anim a in animConditions) a.SetParam(motor.anim);
        }


        public virtual void Deconstruct(Motor motor)
        {
            foreach (Anim a in animConditions) a.ResetParam(motor.anim);
        }
    }
}
