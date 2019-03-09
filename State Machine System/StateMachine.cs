using UnityEngine;
using System.Collections.Generic;


namespace Game.StateMachineSystem
{
    //state machine
    [System.Serializable]
    [CreateAssetMenu(fileName = "State Machine", menuName = "State Machine/State Machine", order = 0)]
    public class StateMachine : ScriptableObject
    {
        //initial state, current state
        public State entry;
        
        //start state machine
        public void StartSM(ISMClient client)
        {
            client.Current = entry;
            client.Current.EnterState(client);
        }

        //update state machine
        public void UpdateSM(ISMClient client)
        {
            GoToNextState(client, client.Current);
        }

        //go to next state, through transitions, if possible
        public void GoToNextState(ISMClient client, SMElement element)
        {
            foreach(SMElement e in element.next)
            {
                if (e.Enter(client))
                {
                    if (e is State)
                        ChangeState(client, (State)e);
                    else GoToNextState(client, e);
                }
            }
        }

        //change state
        public void ChangeState(ISMClient client, State state)
        {
            client.Current.ExitState(client);
            client.Current = state;
            client.Current.EnterState(client);
        }
    }

    //interface for classes that use a state machine
    public interface ISMClient : IClock
    {
        StateMachine StateMachine { get; }
        State Current { get; set; }
    }

    //interface for classes of things that the state machine 'traverses' through
    public abstract class SMElement : ScriptableObject
    {
        //list of state machine elements to transint to next
        public List<SMElement> next = new List<SMElement>();

        //returns true if succefully entered SM element 
        public abstract bool Enter(ISMClient client);
    }
}