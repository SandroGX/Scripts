using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SODictionary : ScriptableObject, ISerializationCallbackReceiver
{
    Dictionary<ScriptableObject, ScriptableObject> dic;


    public void Add(ScriptableObject key, ScriptableObject value)
    {
        SODatabase.Add(this, value, key, dic);
        EditorUtility.SetDirty(this);
    }

    public void Remove(ScriptableObject key, ScriptableObject value)
    {
        SODatabase.Remove(this, value, key, dic);
        EditorUtility.SetDirty(this);
    }

    public T Get<T>(ScriptableObject key) where T : ScriptableObject
    {
        return dic[key] as T;
    }

    public bool ContainsKey(ScriptableObject key)
    {
        return dic.ContainsKey(key);
    }

    //for serialization
    List<ScriptableObject> keys, values;

    public void OnAfterDeserialize()
    {
        dic = new Dictionary<ScriptableObject, ScriptableObject>();
        for (int i = 0; i < keys.Count; ++i)
            dic.Add(keys[i], values[i]);
        keys = values = null;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<ScriptableObject>();
        values = new List<ScriptableObject>();
        foreach (ScriptableObject key in dic.Keys)
            keys.Add(key);
        foreach (ScriptableObject value in dic.Values)
            values.Add(value);
        dic = null;
    }
}