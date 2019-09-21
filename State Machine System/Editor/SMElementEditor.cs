using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GX.StateMachineSystem
{
    //inherit if doing a element editor
    public abstract class SMElementEditor : ScriptableObject
    {
        public StateMachineEditor sm;

        public abstract List<SMElement> NextElem { get; } //list of sm elements that are after this one
        public abstract List<SMElementEditor> Back { get; } //list of sm element editors that are before this one
        public abstract List<SMElementEditor> Next { get; } //list of sm element editors that are after this one

        public abstract Vector2 Position { get; } //position in editor

        public abstract void Draw(); //draw element representation
        public abstract int DrawOrder(); //get draw order
        public abstract void Drag(Vector2 delta); //drag element by delta
        public abstract bool IsIn(Vector2 mousePosition); //is mouse position in element
        public abstract void Menu(GenericMenu menu); //menu to show when right clicked

        public virtual void Save() { EditorUtility.SetDirty(this); } //save changes

        //Add and remove methods for Next and Back elements
        protected virtual void AddBack(SMElementEditor e) { Back.Add(e); }
        protected virtual void AddNext(SMElementEditor e) { Next.Add(e); }
        protected virtual void RemoveBack(SMElementEditor e) { Back.Remove(e); }
        protected virtual void RemoveNext(SMElementEditor e) { Next.Remove(e); }

        protected void AddRangeBack(IEnumerable<SMElementEditor> le) { foreach (SMElementEditor e in le) AddBack(e); }
        protected void AddRangeNext(IEnumerable<SMElementEditor> le) { foreach (SMElementEditor e in le) AddNext(e); }
        protected void RemoveRangeBack(IEnumerable<SMElementEditor> le) { foreach (SMElementEditor e in le) RemoveBack(e); }
        protected void RemoveRangeNext(IEnumerable<SMElementEditor> le) { foreach (SMElementEditor e in le) RemoveNext(e); }
    }
}