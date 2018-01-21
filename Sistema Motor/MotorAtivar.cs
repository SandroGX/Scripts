using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    public class MotorAtivar : StateMachineBehaviour
    {
        Motor motor;
        public string toChange;
        public bool  isToActivate, onEnter, onExit;

        //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            motor = animator.GetComponent<Motor>();

            if (onEnter)
            {
                if (isToActivate) motor.AtivarTodosComNome(toChange);
                else motor.DesativarTodosComNome(toChange);
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}

        //OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (onExit)
            {
                if(isToActivate) motor.AtivarTodosComNome(toChange);
                else motor.DesativarTodosComNome(toChange);
            }
        }

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}

        // OnStateMachineEnter is called when entering a statemachine via its Entry Node
        //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
        //
        //}

        // OnStateMachineExit is called when exiting a statemachine via its Exit Node
        //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
        //
        //}
    }
}
