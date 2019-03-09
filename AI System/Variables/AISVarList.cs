using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

namespace Game.AISystem
{
    [System.Serializable]
    public class AISVarList : AISVariable
    {
        int size;
        public List<int> @int = new List<int>();
        public List<float> @float;
        public List<Vector3> vector3;
        public List<Object> @object;
        public List<IAISVariable> variable;

        public void PassToSingle(AISVarSingle var, int idx)
        {
            if (@int != null && @int.Count != 0 && idx >= 0 && idx < @int.Count) var.@int = @int[idx];

            if (@float != null && @float.Count != 0 && idx >= 0 && idx < @float.Count) var.@float = @float[idx];

            if (vector3 != null && vector3.Count != 0 && idx >= 0 && idx < vector3.Count) var.vector3 = vector3[idx];

            if (@object != null && @object.Count != 0 && idx >= 0 && idx < @object.Count) var.@object = @object[idx];

            if (variable != null && variable.Count != 0 && idx >= 0 && idx < variable.Count) var.variable = variable[idx];
        }

#if UNITY_EDITOR

        public override void InspectorGui()
        {
            size = EditorGUILayout.DelayedIntField("Size:", size);
            System.Type type = System.Type.GetType(this.type);
            if (type.Equals(typeof(int))) ListInt();
            else if (type.Equals(typeof(float))) ListFloat();
            else if (type.Equals(typeof(Vector3))) ListVector3();
            else if (type.IsSubclassOf(typeof(Object))) ListObject(type);
        }

        public override List<string> GetVarTypes()
        {
            return AISEditorUtil.allVarType;
        }

        protected override string VarType() { return "List"; }

        private void ListInt()
        {
            if (size != @int.Count)
            {
                if (size > @int.Count)
                    @int.AddRange(new int[size - @int.Count]);
                else @int.RemoveRange(size, @int.Count - size);
            }

            for (int i = 0; i < size; ++i) @int[i] = EditorGUILayout.IntField("Elemet " + i, @int[i]);
        }

        private void ListFloat()
        {
            if (size != @float.Count)
            {
                if (size > @float.Count)
                    @float.AddRange(new float[size - @float.Count]);
                else @float.RemoveRange(size, @float.Count - size);
            }

            for (int i = 0; i < size; ++i) @float[i] = EditorGUILayout.FloatField("Elemet " + i, @float[i]);
        }

        private void ListVector3()
        {
            if (size != vector3.Count)
            {
                if (size > vector3.Count)
                    vector3.AddRange(new Vector3[size - vector3.Count]);
                else vector3.RemoveRange(size, vector3.Count - size);
            }

            for (int i = 0; i < size; ++i) vector3[i] = EditorGUILayout.Vector3Field("Elemet " + i, vector3[i]);
        }

        private void ListObject(System.Type type)
        {
            if (size != @object.Count)
            {
                if (size > @object.Count)
                    @object.AddRange(new Object[size - @object.Count]);
                else @int.RemoveRange(size, @object.Count - size);
            }

            for (int i = 0; i < size; ++i) @object[i] = EditorGUILayout.ObjectField("Elemet " + i, @object[i], type, true);
        }

#endif
    }
}