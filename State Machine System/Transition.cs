using UnityEngine;
using System.Collections.Generic;

namespace Game.StateMachineSystem
{
    //transition in state machine
    [System.Serializable]
    [CreateAssetMenu(fileName = "Transition", menuName = "State Machine/Transition", order = 0)]
    public class Transition : SMElement
    {
        //list of conditions
        public List<TransitionCondition> conditions = new List<TransitionCondition>();

        //can this transition be traversed
        public bool CanTraverse(ISMClient client)
        {
            foreach (TransitionCondition con in conditions)
                if (!con.ConditionMet(client))
                    return false;

            return true;
        }

        public override bool Enter(ISMClient client)
        {
            return CanTraverse(client);
        }
    }
}