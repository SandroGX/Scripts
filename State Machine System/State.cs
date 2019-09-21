using System.Collections.Generic;

namespace Game.StateMachineSystem
{
    //a state
    [System.Serializable]
    public class State : SMElement
    {
        //behaviour list
        public List<StateBehaviour> behaviours = new List<StateBehaviour>();

        //make client enter the state
        public void EnterState(SMClient client)
        {
            foreach (StateBehaviour bhv in behaviours)
                bhv.OnStateEnter(client);
        }

        //make client exit the state
        public void ExitState(SMClient client)
        {
            foreach (StateBehaviour bhv in behaviours)
                bhv.OnStateExit(client);
        }

        public override State Enter(SMClient client) { return this; }

        public void StartClient(SMClient client)
        {
            foreach (StateBehaviour behaviour in client.CurrentState.behaviours)
                behaviour.OnClientStart(client);
        }

        public void StopClient(SMClient client)
        {
            foreach (StateBehaviour behaviour in client.CurrentState.behaviours)
                behaviour.OnClientStop(client);
        }
    }
}