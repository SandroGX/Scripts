using UnityEngine;
using System.Collections.Generic;
using GX.VarSystem;


namespace GX.StateMachineSystem
{
    //state machine
    [System.Serializable]
    [CreateAssetMenu(fileName = "SM", menuName = "State Machine", order = 0)]
    public class StateMachine : ScriptableObject
    {
        //initial state
        public State entry;

        //template for client
        public VarHandleTemplate variables;


        //use to create client
        public SMClient CreateClient(object varHolder)
        {
            SMClient client = CreateInstance<SMClient>();
            client.Init(this, varHolder);
            return client;
        }

        /*
        //start state machine
        public void StartSM(SMClient client)
        {
            client.StateMachineUpdate = CoroutineAux.StartCoroutineLoop(UpdateSM, null, client);

            //for 1st start
            if (!client.CurrentState)
            {
                client.CurrentState = entry;
                client.CurrentState.EnterState(client);
            }
            else //for restart
                client.CurrentState.StartClient(client);
        }

        //stop state machine on client
        public void StopSM(SMClient client)
        {
            client.CurrentState.StopClient(client);

            GameManager.StopCoroutineGM(client.StateMachineUpdate);
            client.StateMachineUpdate = null;
        }

        //update state machine
        private void UpdateSM(SMClient client)
        {
            GoToNextState(client);
        }

        //go to next state, through transitions, if possible
        private void GoToNextState(SMClient client)
        {
            foreach(SMElement e in client.CurrentState.next)
            {
                if (e.Enter(client))
                    return;
            }
        }
        */
    }


    //class manipulated by the state machine
    public class SMClient : ScriptableObject
    {
        public State CurrentState { get; set; }
        public VarHandle Variables { get; set; }
        public StateMachine SM {get; set;}
        public Coroutine StateMachineUpdate { get; set; }

        public void Init(StateMachine sm, object varHolder)
        {
            SM = sm;
            Variables = SM.variables.Build(varHolder);
        }

        //start state machine
        public void StartSM()
        {
            if (StateMachineUpdate != null)
                return;

            StateMachineUpdate = CoroutineAux.StartCoroutineLoop(UpdateSM, null);

            //for 1st start
            if (!CurrentState)
            {
                CurrentState = SM.entry;
                CurrentState.EnterState(this);
            }
            else //for restart
                CurrentState.StartClient(this);
        }

        //stop state machine on client
        public void StopSM()
        {
            if (StateMachineUpdate == null)
                return;

            CurrentState.StopClient(this);

            GameManager.StopCoroutineGM(StateMachineUpdate);
            StateMachineUpdate = null;
        }

        //update state machine
        private void UpdateSM()
        {
            GoToNextState();
        }

        //go to next state, through transitions, if possible
        private void GoToNextState()
        {
            State s;
            foreach (SMElement e in CurrentState.next)
            {
                if (s = e.Enter(this))
                {
                    ChangeState(s);
                    return;
                }
            }
        }

        //change state
        public void ChangeState(State state)
        {
            CurrentState.ExitState(this);
            CurrentState = state;
            CurrentState.EnterState(this);
        }

        public static implicit operator VarHandle(SMClient c) => c.Variables;
    }


    //abstract class for classes of things that the state machine 'traverses' through
    public abstract class SMElement : ScriptableObject
    {
       
        //list of state machine elements to transint to next
        public List<SMElement> next = new List<SMElement>();

        //returns the next state
        public abstract State Enter(SMClient client);
    }
}