using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.SistemaMotor;

[System.Serializable]
public class Control
{
    [HideInInspector]
    public Player player;
    public enum VerbType { In, On, Off }
    public VerbType verb;

    public MotorEstado defaultState;
    public MotorEstado exitState;
    public Dictionary<MotorEstado, MotorEstado> nextState = new Dictionary<MotorEstado, MotorEstado>();

    public string control;
    public float comboTimeMax;


    public void Action(Motor motor)
    {
        //if (player.motor.currentState != defaultState && !nextState.ContainsKey(player.motor.currentState))
        //{
        //    if (player.motor.nextState == defaultState || nextState.ContainsValue(player.motor.nextState)) player.motor.nextState = null;

        //    switch (verb)
        //    {
        //        case VerbType.In: if (!Input.GetButtonDown(control)) return; break;
        //        case VerbType.On: if (!Input.GetButton(control)) return; break;
        //        case VerbType.Off: if (!Input.GetButtonUp(control)) return; break;
        //    }

        //    if (nextState.ContainsKey(player.motor.currentState)) player.motor.nextState = nextState[player.motor.currentState];
        //    else player.motor.nextState = defaultState;

        //    player.time = 0;
        //}
        //else
        //{
        //    switch (verb)
        //    {
        //        case VerbType.In: if (Input.GetButtonDown(control)) player.time = 0; break;
        //        case VerbType.Off: if (Input.GetButtonUp(control)) player.time = 0; break;
        //        case VerbType.On: if (!Input.GetButton(control)) player.motor.nextState = exitState; break;
        //    }

        //    if (player.time > comboTimeMax && verb != VerbType.On) player.motor.nextState = exitState;
        //}

        if (A()) { motor.nextState = Next(motor.currentState); player.time = 0; }

        if (player.time > comboTimeMax)
        {
            if (motor.currentState == defaultState || nextState.ContainsValue(motor.currentState)) motor.nextState = exitState;
            if (motor.nextState == defaultState || nextState.ContainsValue(motor.nextState)) motor.nextState = motor.currentState;
        }
    }


    MotorEstado Next(MotorEstado current)
    {
        if (nextState.ContainsKey(current)) return nextState[current];
        else return defaultState;
    }


    bool A()
    {
        switch (verb)
        {
            case VerbType.In: return Input.GetButtonDown(control); break;
            case VerbType.Off: return Input.GetButtonUp(control); break;
            case VerbType.On: return Input.GetButton(control); break;
        }

        return false;
    }
}
