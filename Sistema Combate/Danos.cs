using Game;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public struct Danos
{
    public int danos;


    public static Danos operator +(Danos A, Danos B)
    {
        return new Danos(A.danos + B.danos);
    }

    public static Danos operator -(Danos A, Danos B)
    {
        return new Danos(A.danos - B.danos);
    }

    public Danos(int danos)
    {
        this.danos = danos;
    }

#if UNITY_EDITOR

    public void Gui()
    {
        danos = EditorGUILayout.IntField("Danos: ", danos);
    }

#endif 

}
