using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.MotorSystem;

[System.Serializable]
public class Control
{
    [HideInInspector]
    public Player player;
    public enum VerbType { In, On, Off }
    public VerbType verb;

    public MotorState defaultState;
    public MotorState exitState;
    public Dictionary<MotorState, MotorState> nextState = new Dictionary<MotorState, MotorState>();

    public string control;
    public float comboTimeMax;


    public void Action(Motor motor)
    {

        if (A()) { motor.nextState = Next(motor.currentState); player.time = 0; }

        if (player.time > comboTimeMax)
        {
            if (motor.currentState == defaultState || nextState.ContainsValue(motor.currentState)) motor.nextState = exitState;
            if (motor.nextState == defaultState || nextState.ContainsValue(motor.nextState)) motor.nextState = motor.currentState;
        }
    }


    MotorState Next(MotorState current)
    {
        if (nextState.ContainsKey(current)) return nextState[current];
        else return defaultState;
    }


    bool A()
    {
        switch (verb)
        {
            case VerbType.In: return Input.GetButtonDown(control); 
            case VerbType.Off: return Input.GetButtonUp(control); 
            case VerbType.On: return Input.GetButton(control);
        }

        return false;
    }
}
