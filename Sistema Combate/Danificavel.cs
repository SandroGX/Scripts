using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Danificavel
{
    public Statistic life = new Statistic();

    public static implicit operator bool(Danificavel danificavel)
    {
        if (danificavel != null) return true;
        else return false;
    }


    public void ReceiveDamage(Danos damage)
    {
        life.Add(-damage.danos);
    }


    public Danificavel(Statistic life)
    {
        this.life = new Statistic(life);
    }

    public Danificavel(Danificavel original)
    {
        this.life = new Statistic(original.life);
    }

    public Danificavel()
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
