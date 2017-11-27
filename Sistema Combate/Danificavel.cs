using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Danificavel
{
    public int danos, danosMax, varDanos;

    public event System.Action Destruido;



    public virtual void ReceberDanos(Danos danosAReceber)
    {
        danos += danosAReceber.danos;

        if (danos >= danosMax && Destruido != null)
            Destruido();
    }



    public static implicit operator bool(Danificavel danificavel)
    {
        if (danificavel != null)
            return true;
        else return false;
    }



    public Danificavel(Danificavel danificavel)
    {
        this.danos = danificavel.danos;
        this.danosMax = danificavel.danosMax;
        this.varDanos = danificavel.varDanos;
    }

    public Danificavel(int danos, int danosMax, int varDanos)
    {
        this.danos = danos;
        this.danosMax = danosMax;
        this.varDanos = varDanos;
    }


#if UNITY_EDITOR
    public void Gui()
    {
        danosMax = EditorGUILayout.IntField("Danos Max: ", danosMax);
        varDanos = EditorGUILayout.IntField("Danos Var: ", varDanos);
    }
#endif
}
