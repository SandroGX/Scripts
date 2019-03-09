using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Game.StateMachineSystem
{
    //a state
    [System.Serializable]
    [CreateAssetMenu(fileName = "State", menuName = "State Machine/State", order = 0)]
    public class State : SMElement
    {
        //behaviour list
        public List<StateBehaviour> behaviours = new List<StateBehaviour>();

        //make client enter the state
        public void EnterState(ISMClient client)
        {
            foreach (StateBehaviour bhv in behaviours)
                bhv.OnStateEnter(client);
        }

        //make client exit the state
        public void ExitState(ISMClient client)
        {
            foreach (StateBehaviour bhv in behaviours)
                bhv.OnStateExit(client);
        }

        public override bool Enter(ISMClient client) { return true; }
    }
}