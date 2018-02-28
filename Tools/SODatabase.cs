using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class SODatabase
{
    //static System.Object;

    public static void Add<P, C>(P parent, C newObject, List<C> children) where P : ScriptableObject where C : ScriptableObject
    {
        AssetDatabase.AddObjectToAsset(newObject, parent);
        children.Add(newObject);
    }

    //public static void Save()

}