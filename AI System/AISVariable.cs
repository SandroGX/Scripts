using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

namespace GX.AISystem
{
    [Serializable]
    public abstract class AISVariable : ScriptableObject
    {
        public string type;
        public bool isStatic;

        public virtual void Init(AISController ctrl) { }

#if UNITY_EDITOR

        public AISVariable original;

        public void OnGui()
        {
            name = EditorGUILayout.TextField("Name", name);

            EditorGUILayout.LabelField(VarType());
            isStatic = EditorGUILayout.Toggle("isStatic?", isStatic);

            if (type == null || !AISEditorUtil.allVarType.Contains(type)) type = AISEditorUtil.allVarType[0];
            type = AISEditorUtil.allVarType[EditorGUILayout.Popup("Type:", AISEditorUtil.allVarType.IndexOf(type), GetVarTypes().Select(x => Type.GetType(x).Name).ToArray())];

            if (isStatic) InspectorGui();
        }

        public abstract void InspectorGui();

        protected abstract string VarType();
        public abstract List<string> GetVarTypes();

#endif
    }
}
