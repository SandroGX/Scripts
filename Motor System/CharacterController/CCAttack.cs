using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Motor/CharacterController/Attack", order = 0)]
    public class CCAttack : CCWalk
    {
        Dictionary<Motor, bool> waiting = new Dictionary<Motor, bool>();

        public override void ProcessMovement(Motor motor)
        {
            if (waiting[motor] && motor.nextState == this && !motor.character.tired) Attack(motor);

            base.ProcessMovement(motor);
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
    }
}
