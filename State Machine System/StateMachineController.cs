using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem
{
    public class StateMachineController : MonoBehaviour, ISMClient
    {
        public StateMachine stateMachine;
        public StateMachine StateMachine { get { return stateMachine; } }
        public State Current { get; set; }

        private void Start()
        {
            stateMachine.StartSM(this);
        }

        private void Update()
        {
            stateMachine.UpdateSM(this);
        }
    }
}