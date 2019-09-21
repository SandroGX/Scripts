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

    public static void Remove<P, C>(P parent, C toRemove, List<C> children) where P : ScriptableObject where C : ScriptableObject
    {
        children.Remove(toRemove);
        ScriptableObject.DestroyImmediate(toRemove, true);
    }

    public static void Add<P, V, K>(P parent, V newObject, K key, Dictionary<K, V> children) where P : ScriptableObject where V : ScriptableObject
    {
        AssetDatabase.AddObjectToAsset(newObject, parent);
        children.Add(key, newObject);
    }

    public static void Remove<P, V, K>(P parent, V newObject, K key, Dictionary<K, V> children) where P : ScriptableObject where V : ScriptableObject
    {
        children.Remove(key);
        ScriptableObject.DestroyImmediate(newObject, true);
    }

    //public static void Save()
}
#endif