using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "State Bhv Ticked Test", menuName = "State Machine/State Bhv Ticked Test", order = 0)]
    public class StateBhvTickedTest : StateBehaviourTicked
    {
        public string test;

        protected override void OnState(ISMClient client)
        {
            Debug.Log("Tick: " + test);
        }
    }
}