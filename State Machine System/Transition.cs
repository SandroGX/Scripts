using System.Collections.Generic;

namespace GX.StateMachineSystem
{
    //transition in state machine
    [System.Serializable]
    public class Transition : SMElement
    {
        //list of conditions
        public List<TransitionCondition> conditions = new List<TransitionCondition>();

        //can this transition be traversed
        public bool CanTraverse(SMClient client)
        {
            foreach (TransitionCondition con in conditions)
                if (!con.ConditionMet(client))
                    return false;
            return true;
        }

        public override State Enter(SMClient client)
        {
            State s;
            if (CanTraverse(client))
                foreach (SMElement e in next)
                    if (s = e.Enter(client))
                        return s;
            return null;
        }
    }
}