using UnityEngine;

namespace GX.StateMachineSystem
{
    //A behaviour of a state
    public abstract class StateBehaviour : ScriptableObject
    {
        //what it does on entering state
        public virtual void OnStateEnter(SMClient client) { }
        //what it does on leaving state
        public virtual void OnStateExit(SMClient client) { }
        //what it does when client is stopped
        public virtual void OnClientStop(SMClient client) { }
        //what it does when client is started with this behaviour in current state
        public virtual void OnClientStart(SMClient client) { }
    }
}