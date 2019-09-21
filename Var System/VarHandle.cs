using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.VarSystem
{
    [System.Serializable]
    public class VarHandleTemplate : IEnumerable<Variable>
    {
        //list of variables
        [SerializeField]
        private List<Variable> variables;

        //constructor to create a template (maybe make its own class)
        public VarHandleTemplate() { variables = new List<Variable>(); }
        
        public void Add(System.Type varType)
        {
            if (!varType.IsSubclassOf(typeof(Variable)))
                return;

            variables.Add((Variable)Variable.CreateInstance(varType));
        }

        public void Remove(int idx)
        {
            variables.RemoveAt(idx);
        }

        public VarHandle Build(object varHolder)
        {
            return new VarHandle(this, varHolder);
        }

        public IEnumerator<Variable> GetEnumerator() => variables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)variables).GetEnumerator();
        
    }

    [System.Serializable]
    public class VarHandle
    {
        //list of variables
        [SerializeField]
        private List<Variable> variables;

        public VarHandle(VarHandleTemplate template, object varHolder)
        {
            variables = new List<Variable>();
            foreach (Variable v in template)
            {
                Variable x = v.Duplicate(varHolder);
                variables.Add(x);
            }
        }
        
    
        public V Get<V>(int idx) { return variables[idx].Get<V>(); }
        public void Set<V>(int idx, V v) { variables[idx].Set(v); }
    }

    //container for variables
    [System.Serializable]
    public abstract class Variable : ScriptableObject
    {
        [SerializeField]
        protected object va;
        public bool isStatic, isReadOnly;

        public abstract Variable Duplicate(object varHolder);
        public virtual V Get<V>() { return (V)va; }
        public virtual void Set<V>(V v)
        {
            if (!isReadOnly)
                va = v;
            else Debug.LogWarning("Tried to modify readonly variable " + name);
        }
    }

    public struct VarAccesser<V>
    {
        public int idx;

        public V this[VarHandle v]
        {
            get => v.Get<V>(idx);
            set => v.Set(idx, value);
        }
    }
}
