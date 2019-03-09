#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class SODatabase
{

    public static void Add<P, C>(P parent, C newObject, List<C> children) where P : ScriptableObject where C : ScriptableObject
    {
        AssetDatabase.AddObjectToAsset(newObject, parent);
        children.Add(newObject);
    }

    public static void Add<P, C, K>(P parent, C newObject, K key, Dictionary<K, C> children) where P : ScriptableObject where C : ScriptableObject
    {
        AssetDatabase.AddObjectToAsset(newObject, parent);
        children.Add(key, newObject);
    }

    //public static void Save()
}
#endif