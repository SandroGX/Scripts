using UnityEngine;

namespace Game.StateMachineSystem
{
    [System.Serializable]
    public class StateBhvTickedTest : StateBehaviourTicked
    {
        public string test;

        public GenericYieldInstructionGetter yield;
        protected override object YieldInstruction { get => new WaitForSeconds(1);/*yield.GetYieldInstruction();*/ }

        protected override void OnState(SMClient client)
        {
            Debug.Log("Tick: " + test);
        }
    }
}