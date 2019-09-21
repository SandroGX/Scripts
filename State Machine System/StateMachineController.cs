using UnityEngine;
using GX.VarSystem;

namespace GX.StateMachineSystem
{
    public class StateMachineController : MonoBehaviour
    {
        public StateMachine stateMachine;

        public SMClient Client { get; private set; }

        private void Awake()
        {
            Client = stateMachine.CreateClient(this);
        }

        private void OnEnable()
        {
            Client.StartSM();
        }

        private void OnDisable()
        {
            Client.StopSM();
        }

        private void OnDestroy()
        {
            Client.StopSM();
        }
    }

    public class VarComponent : Variable
    {
        public System.Type compType;

        public override Variable Duplicate(object varHolder)
        {
            if (isStatic)
            {
                if(va == null)

                return this;
            }

            VarComponent v = CreateInstance<VarComponent>();
            v.va = ((Component)varHolder).GetComponent(compType);
            return v;
        }
    }
}