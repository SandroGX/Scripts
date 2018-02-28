using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.MotorSystem;

[CreateAssetMenu(fileName = "AnimIncrs", menuName = "Motor/AnimIncrs", order = 2)]
public class AnimIncrs : AnimState
{
    public string param;
    public int max;
    Dictionary<Motor, int> rep = new Dictionary<Motor, int>();

    public override void OnAnimationEnd(Motor motor)
    {

        if (motor.nextState == this) PlayConditions(motor);
        else
        {
            rep.Remove(motor);
            base.OnAnimationEnd(motor);
        }
    }



    protected override void PlayConditions(Motor motor)
    {
        if (!rep.ContainsKey(motor)) rep.Add(motor, 0);

        motor.anim.SetInteger(param, rep[motor]);

        base.PlayConditions(motor);

        if (++rep[motor] >= max) rep[motor] = 0;
    }

}
