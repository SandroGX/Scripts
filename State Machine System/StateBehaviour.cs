using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem
{
    //A behaviour of a state
    public abstract class StateBehaviour : ScriptableObject
    {
        //initialization 
        //uncomment when doing editor
        //public abstract void Init();

        //what it does on entering state
        public abstract void OnStateEnter(ISMClient client);
        //what it does on leaving state
        public abstract void OnStateExit(ISMClient client);
    }
}