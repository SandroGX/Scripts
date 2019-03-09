using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Condition Test", menuName = "State Machine/Condition Test", order = 0)]
    public class ConditionTest : TransitionCondition
    {
        public bool test;
        public override bool ConditionMet(ISMClient client)
        {
            return test;
        }
    }
}