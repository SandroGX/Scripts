using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
[CanEditMultipleObjects]
public class SpawnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Spawner d = (Spawner)target;

        if (GUILayout.Button("Spawn"))
            d.Spawn();

        if (GUILayout.Button("Retirar"))
            d.CleanChildren();
    }
}
