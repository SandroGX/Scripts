using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GX.MotorSystem;

[CreateAssetMenu(fileName = "AnimIncrs", menuName = "Motor/AnimIncrs", order = 2)]
public class AnimIncrs : AnimState
{
    public string param;
    public int max;
    Dictionary<Motor, int> rep = new Dictionary<Motor, int>();


    protected override void PlayConditions(Motor motor)
    {
        if (!rep.ContainsKey(motor)) rep.Add(motor, 0);

        motor.anim.SetInteger(param, rep[motor]);

        base.PlayConditions(motor);

        if (++rep[motor] >= max) rep[motor] = 0;
    }

}
