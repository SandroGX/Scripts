using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Motor/CharacterController/Attack", order = 0)]
    public class CCAttack : CCAndar
    {
        Dictionary<Motor, bool> waiting = new Dictionary<Motor, bool>();

        public override void ProcessMovement(Motor motor)
        {
            if (waiting[motor] && motor.nextState == this && !motor.character.tired) Attack(motor);

            base.ProcessMovement(motor);
        }


        public override void OnAnimationEnd(Motor motor)
        {
            if (!waiting[motor]) waiting[motor] = true;
            else motor.ChangeState(Transition(motor));
        }


        public override void Construct(Motor motor)
        {
            motor.character.stamina.varValue += staminaVar;
            base.Construct(motor);
            waiting.Add(motor, false);
            Attack(motor);
        }


        public override void Deconstruct(Motor motor)
        {
            motor.character.stamina.varValue -= staminaVar;
            base.Deconstruct(motor);
            waiting.Remove(motor);
        }


        void Attack(Motor motor)
        {
            waiting[motor] = false;
            motor.anim.SetTrigger("Attack");
            motor.character.stamina.value -= staminaVar;
        }


        public override bool CanStay(Motor motor) { return base.CanStay(motor) && !waiting.ContainsKey(motor); }


        //public override MotorEstado Transition(Motor motor) { return null; }

        public override MotorEstado GetNextState(Motor motor)
        {
            return null;
        }


    }
}
