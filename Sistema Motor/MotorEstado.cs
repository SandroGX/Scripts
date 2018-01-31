using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.MotorSystem {
    public abstract class MotorEstado : ScriptableObject
    {
        public bool animExit;
        public Anim[] animConditions;


        [SerializeField]
        public List<MotorEstado> passiveStates = new List<MotorEstado>();

        public abstract void ProcessMovement(Motor motor);


        public virtual void OnAnimationEnd(Motor motor)
        {
            if (animExit) motor.ChangeState(Transition(motor));    
        }


        public virtual void Construct(Motor motor)
        {
            foreach (Anim a in animConditions) a.SetParam(motor.anim);
        }


        public virtual void Deconstruct(Motor motor)
        {
            foreach (Anim a in animConditions) a.ResetParam(motor.anim);
        }


        public abstract bool CanStay(Motor motor);


        public virtual MotorEstado Transition(Motor motor)
        {
            if (motor.nextState && motor.nextState != this && motor.nextState.CanStay(motor)) return motor.nextState;

            foreach (MotorEstado m in passiveStates)
            {
                if (m.CanStay(motor)) return m;
            }

            if (!CanStay(motor)) return motor.defaultState;

            return null;
        }


        public virtual MotorEstado GetNextState(Motor motor)
        {
            if (!animExit) return Transition(motor);
            else return null;
        }
    }
}
