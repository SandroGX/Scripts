using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;


namespace Game.AISystem
{
    [Serializable]
    public abstract class AISVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        public Type type;
        public bool isStatic;

        #region Serialization
        [SerializeField] string typeName;
        public void OnAfterDeserialize()
        {
            type = Type.GetType(typeName);
        }

        public void OnBeforeSerialize()
        {
            if(type != null) typeName = type.FullName;
        }
        #endregion

#if UNITY_EDITOR

        public void OnGui()
        {
            name = EditorGUILayout.TextField("Name", name);

            EditorGUILayout.LabelField(this as AISVarList ? " List" : "Single");
            isStatic = EditorGUILayout.Toggle("isStatic?", isStatic);

            if (type == null || !AISEditorUtil.allVarType.Contains(type)) type = AISEditorUtil.allVarType[0];
            type = AISEditorUtil.allVarType[EditorGUILayout.Popup("Type:", AISEditorUtil.allVarType.IndexOf(type), AISEditorUtil.allVarType.Select(x => x.Name).ToArray())];
        }


        public abstract void InspectorGui();

#endif
    }
}
