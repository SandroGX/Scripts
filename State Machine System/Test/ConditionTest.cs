using UnityEngine;
using UnityEditor;

namespace GX.StateMachineSystem
{
    [System.Serializable]
    public class ConditionTest : TransitionCondition
    {
        public bool test;
        public override bool ConditionMet(SMClient client)
        {
            return test;
        }
    }
}