using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "State Behaviour Test", menuName = "State Machine/State Behaviour Test", order = 0)]
    public class StateBehaviourTest : StateBehaviour
    {
        public string test;

        /*
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
        */

        public override void OnStateEnter(ISMClient client)
        {
            Debug.Log("Hi, entered. " + test);
        }

        public override void OnStateExit(ISMClient client)
        {
            Debug.Log("Bye, left" + test);
        }
    }
}