using Game;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public struct Damage
{
    public int damage;


    public static Damage operator +(Damage A, Damage B)
    {
        return new Damage(A.damage + B.damage);
    }

    public static Damage operator -(Damage A, Damage B)
    {
        return new Damage(A.damage - B.damage);
    }

    public Damage(int danos)
    {
        this.damage = danos;
    }

#if UNITY_EDITOR

    public void Gui()
    {
        damage = EditorGUILayout.IntField("Danos: ", damage);
    }

#endif 

}
