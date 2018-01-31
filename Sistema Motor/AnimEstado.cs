using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "Anim", menuName = "Motor/Anim", order = 0)]
    public class AnimEstado : MotorEstado
    {
        public Anim[] condicoes;
        public MovimentoBasico movimento;
        //MovimentoBasico m;


        public override void OnAnimationEnd(Motor motor)
        {
            motor.ChangeState(motor.nextState);
        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            PlayConditions(motor);
            //m = movimento;
        }


        public override void ProcessMovement(Motor motor)
        {
            /*MotorEstado n = m.Transition(motor);
            if (n != null)  m = n;*/ 
            if(movimento) movimento.ProcessMovement(motor);
        }


        protected virtual void PlayConditions(Motor motor)
        {
            foreach (Anim condicao in condicoes) condicao.SetParam(motor.anim);
        }

        public override bool CanStay(Motor motor)
        {
            return movimento&&movimento.CanStay(motor) || !movimento;
        }

        public override MotorEstado GetNextState(Motor motor)
        {
            return null;
        }
    }
}
