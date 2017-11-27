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
    //public List<float> multiplicadores = new List<float>();

    //public int Dano
    //{
    //    get
    //    {
    //        float d = danos;

    //        foreach (float m in multiplicadores)
    //            d *= m;

    //        return Mathf.FloorToInt(d);
    //    }
    //}

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
