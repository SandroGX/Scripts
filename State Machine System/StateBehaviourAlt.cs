

namespace GX.StateMachineSystem
{
    public abstract class StateBehaviourAlt : StateBehaviourTicked
    {
        public enum E { OnEnter, OnTick, OnExit }
        public E e;


        public override void OnStateEnter(SMClient client)
        {
            if (e == E.OnEnter)
                OnState(client);
            else if(e == E.OnTick)
                base.OnStateEnter(client);
        }

        public override void OnStateExit(SMClient client)
        {
            if (e == E.OnExit)
                OnState(client);
            else if (e == E.OnTick)
                base.OnStateExit(client);
        }

        public override void OnClientStart(SMClient client)
        {
            if(e == E.OnTick)
                base.OnClientStart(client);
        }

        public override void OnClientStop(SMClient client)
        {
            if (e == E.OnTick)
                base.OnClientStart(client);
        }
    }
}
