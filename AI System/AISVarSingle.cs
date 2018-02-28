using UnityEngine;
using UnityEditor;

namespace Game.AISystem
{
    [System.Serializable]
    public class AISVarSingle : AISVariable
    {
        public int @int;
        public float @float;
        public Vector3 vector3;
        public Object @object;
        public IAISVariable variable;


#if UNITY_EDITOR
        public override void InspectorGui()
        {
            if(type.Equals(typeof(int))) @int = EditorGUILayout.IntField("Int:", @int);
            else if(type.Equals(typeof(float))) @float = EditorGUILayout.FloatField("Float:", @float);
            else if(type.Equals(typeof(Vector3))) vector3 = EditorGUILayout.Vector3Field("Vector3:", vector3);
            else if(type.IsSubclassOf(typeof(Object))) @object = EditorGUILayout.ObjectField("Object:", @object, type, true);
        }
#endif
    }
}