using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Damageable
{
    public Statistic life = new Statistic();

    public static implicit operator bool(Damageable damageable)
    {
        if (damageable != null) return true;
        else return false;
    }


    public void ReceiveDamage(Damage damage)
    {
        life.Add(-damage.damage);
    }


    public Damageable(Statistic life)
    {
        this.life = new Statistic(life);
    }

    public Damageable(Damageable original)
    {
        life = new Statistic(original.life);
    }

    public Damageable()
    {
        life = new Statistic();
    }


#if UNITY_EDITOR
    public void Gui()
    {
        life.Gui();
    }
#endif
}
