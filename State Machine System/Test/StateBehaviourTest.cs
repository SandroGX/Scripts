using UnityEngine;
using UnityEditor;

namespace GX.StateMachineSystem
{
    [System.Serializable]
    public class StateBehaviourTest : StateBehaviour
    {
        public string test;

        /*
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
        */

        public override void OnStateEnter(SMClient client)
        {
            Debug.Log("Hi, entered. " + test);
        }

        public override void OnStateExit(SMClient client)
        {
            Debug.Log("Bye, left. " + test);
        }
    }
}