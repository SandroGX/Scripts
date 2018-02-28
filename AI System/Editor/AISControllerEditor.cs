using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Game.AISystem
{
    [CustomEditor(typeof(AISController))]
    public class AISControllerEditor : Editor
    {
        Dictionary<AISVariable, bool> show = new Dictionary<AISVariable, bool>();

        public override void OnInspectorGUI()
        {

            AISController ctrl = (AISController)target;

            ctrl.ai = EditorGUILayout.ObjectField("AI:", ctrl.ai, typeof(AISAI), false) as AISAI;

            if (ctrl.variables == null || ctrl.variables.Count != ctrl.ai.variables.Count || ctrl.variables == ctrl.ai.variables)
            {
                ctrl.SetAIVars(ctrl.ai.variables);
                show.Clear();
            }


            foreach(AISVariable v in ctrl.variables)
            {
                if (v.isStatic) continue;

                if (!show.ContainsKey(v)) show.Add(v, false);
                
                show[v] = EditorGUILayout.Foldout(show[v], v.name);
                if(show[v]) v.InspectorGui();

            }
        }

    }
}