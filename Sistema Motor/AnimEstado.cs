using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "Anim", menuName = "Motor/Anim", order = 0)]
    public class AnimEstado : MotorEstado
    {
        public Anim[] condicoes;


        public override void OnAnimacaoEnd(Motor motor)
        {
            motor.ProximoEstado();
        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            PlayConditions(motor);
        }


        protected virtual void PlayConditions(Motor motor)
        {
            foreach (Anim condicao in condicoes)
                condicao.SetParam(motor.anim);
        }
    }
}
