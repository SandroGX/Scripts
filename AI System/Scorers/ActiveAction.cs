using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AISystem {
    public class ActiveAction : IfTrue
    {

        protected override bool Evaluate(AISController ai)
        {
            if (owner.parent.exe == AISAction.Execution.Parallel || owner.parent.children.Count == 1) return true;
            else if (owner.parent.bestChild.ContainsKey(ai)) return owner.parent.bestChild[ai] == owner;
            else return false;
        }

        
    }
}
