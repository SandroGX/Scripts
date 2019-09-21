using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.AISystem {
    public class ActiveAction : IfTrue
    {
        protected override bool Evaluate(AISController ctrl)
        {
            if (owner.parent.exe == AISAction.Execution.Parallel || owner.parent.children.Count == 1) return true;
            else if (owner.parent.bestChild.ContainsKey(ctrl)) return owner.parent.bestChild[ctrl] == owner;
            else return false;
        } 
    }
}
