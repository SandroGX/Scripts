using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


namespace GX.StateMachineSystem
{
    //state machine
    [System.Serializable]
    public class StateMachineEditor : ScriptableObject
    {
        public static Vector2 minMaxZoom = new Vector2(.2f, 1.8f);
        public StateMachine stateMachine; //the sm this editor is representating
        public List<SMElementEditor> elems = new List<SMElementEditor>(); //all element editor of this sm, 0 -> count-1 to draw, count-1 to 0 to get selected

        public Vector2 drawOffset = Vector2Int.zero, drawCenter;
        [SerializeField]
        private float zoom = 1;

        public float Zoom { set => zoom = Mathf.Clamp(value, minMaxZoom.x, minMaxZoom.y); get => zoom; }

        //create editor for sm
        public static StateMachineEditor CreateEditor(StateMachine sm)
        {
            StateMachineEditor sme = CreateInstance<StateMachineEditor>();
            sme.stateMachine = sm;
            sme.name = sm.name + "Editor";
            AssetDatabase.AddObjectToAsset(sme, sm);
            if (!sm.entry)
            {
                StateEditor s = StateEditor.CreateState(sme, Vector2.zero);
                s.state.name = "Entry State";
                s.name = "Entry State Editor";
                sme.SetEntryState(s);
            }
            sme.Save();
            return sme;
        }

        //add new element editor and in the right order
        public void Add(SMElementEditor element)
        {
            SODatabase.Add(this, element, elems);
            elems.Sort((x, y) => { return x.DrawOrder() - y.DrawOrder(); });
        }
        //remove element from this sm
        public void Remove(SMElementEditor element)
        {
            SODatabase.Remove(this, element, elems);
        }
        //set this sm entry state
        public void SetEntryState(StateEditor s)
        {
            if(elems.Contains(s))
                stateMachine.entry = s.state;
        }
        //save this sm editor, sm, and all other elements
        public void Save()
        {
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(stateMachine);

            foreach (SMElementEditor e in elems)
                e.Save();
        }

        //get selected(the one pos is in), and ignore ignore 
        public SMElementEditor GetSelected(Vector2 pos, SMElementEditor ignore)
        {
            for (int i = elems.Count-1; i >= 0; --i)
                if (elems[i] != ignore && elems[i].IsIn(pos))
                    return elems[i];
            return null;
        }

        public  Vector2 RealPosToDrawPos(Vector2 position)
        {
            return (position + drawOffset) * zoom + drawCenter;
        }

        public Vector2 DrawPosToRealPos(Vector2 position)
        {
            return (position - drawCenter) / zoom - drawOffset;
        }
    }
}