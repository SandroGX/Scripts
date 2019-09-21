using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachineSystem;


namespace Game.MotorSystem
{
    public abstract class MotorState : StateBehaviour
    {
       
        public abstract void ProcessMovement(Motor motor);

        public override void OnStateEnter(SMClient client)
        {
            //if (client is MonoBehaviour)
                //OnStateEnter(((MonoBehaviour)client).GetComponent<Motor>());
        }

        public override void OnStateExit(SMClient client)
        {
            //if (client is MonoBehaviour)
                //OnStateExit(((MonoBehaviour)client).GetComponent<Motor>());
        }

        public abstract void OnStateEnter(Motor motor);

        public abstract void OnStateExit(Motor motor);
    }
}
