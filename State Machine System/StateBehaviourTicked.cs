namespace GX.StateMachineSystem
{
    public abstract class StateBehaviourTicked : StateBehaviour
    {
        
        protected abstract object YieldInstruction { get; }

        
        public override void OnStateEnter(SMClient client) { StartCoroutine(client); }
        
        public override void OnStateExit(SMClient client) { StopCoroutine(client); }

        //what it does on a tick
        protected abstract void OnState(SMClient client);


        public override void OnClientStart(SMClient client) { StartCoroutine(client); }

        public override void OnClientStop(SMClient client) { StopCoroutine(client); }


        private void StartCoroutine(SMClient client)
        {
            CoroutineManager.StoreCoroutine(new Pair<SMClient, StateBehaviour>(client, this),
                CoroutineAux.StartCoroutineLoop(OnState, YieldInstruction, client));
        }

        private void StopCoroutine(SMClient client)
        {
            CoroutineManager.StopCoroutine(new Pair<SMClient, StateBehaviour>(client, this));
        }
    }
}