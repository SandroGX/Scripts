using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.SistemaMotor {
    public abstract class MotorEstado : ScriptableObject
    {
        //[HideInInspector]
        //public MotorEstado original;

        public virtual void ProcessarMovimento(Motor motor)
        {
            
        }


        public virtual void ModificarCharacter(Motor motor)
        {

        }


        public virtual void OnAnimacaoEnd(Motor motor)
        {

        }


        public virtual void Construct(Motor motor)
        {
            Transicao(motor);
        }


        public virtual void Deconstruct(Motor motor)
        {

        }


        public virtual void Transicao(Motor motor)
        {

        }
    }
}
