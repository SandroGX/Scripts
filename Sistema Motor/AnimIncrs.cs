﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.SistemaMotor;

[CreateAssetMenu(fileName = "AnimIncrs", menuName = "Motor/AnimIncrs", order = 2)]
public class AnimIncrs : AnimEstado
{
    public string param;
    public int max;
    Dictionary<Motor, int> rep = new Dictionary<Motor, int>();

    public override void OnAnimacaoEnd(Motor motor)
    {

        if (motor.proximoEstado == this)
            PlayConditions(motor);
        else
        {
            rep.Remove(motor);
            motor.ProximoEstado();
        }
    }



    protected override void PlayConditions(Motor motor)
    {
        if (!rep.ContainsKey(motor))
            rep.Add(motor, 0);

        motor.anim.SetInteger(param, rep[motor]);

        base.PlayConditions(motor);

        if (rep[motor] < max)
            ++rep[motor];
        else rep[motor] = 0;
    }

}