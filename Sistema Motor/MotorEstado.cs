using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.SistemaMotor {
    public abstract class MotorEstado : ScriptableObject
    {
        [SerializeField]
        public List<MotorEstado> passivos = new List<MotorEstado>();

        public virtual void ProcessMovement(Motor motor) { }


        public virtual void OnAnimationEnd(Motor motor) { }


        public virtual void Construct(Motor motor) { }


        public virtual void Deconstruct(Motor motor) { }


        public virtual bool CanStay(Motor motor)
        {
            return false;
        }


        public virtual MotorEstado Transition(Motor motor)
        {
           
            if (motor.nextState && motor.nextState != this && motor.nextState.CanStay(motor)) return motor.nextState;
            
            foreach (MotorEstado m in passivos)
            {
                if (m.CanStay(motor)) return m;
            }

            if (!CanStay(motor)) return motor.defaultState;

            return null;
        }
    }
}
