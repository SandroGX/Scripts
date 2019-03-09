using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem
{
    public abstract class StateBehaviourTicked : StateBehaviour
    {
        Clock<ISMClient> clock;
        public float tick;

        /* 
        public override void Init()
        {
            clock = new Clock<ISMClient>(OnState, tick);
        }
        */

        public override void OnStateEnter(ISMClient client)
        {
            if(clock == null)
                clock = new Clock<ISMClient>(OnState, tick);

            clock.Add(client);
        }
        

        public override void OnStateExit(ISMClient client)
        {
            clock.Remove(client);
        }

        //what it does on a tick
        protected abstract void OnState(ISMClient client);
    }
}