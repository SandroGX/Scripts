using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Game.StateMachineSystem
{
    //editor representing a state
    public class StateEditor : SMElementEditor
    {
        public static readonly Vector2 size = new Vector2(200, 50); //size of box representing state
        private static readonly GUIContent setEntryGui = new GUIContent("Set Entry"), deleteGui = new GUIContent("Delete");
        private static readonly Color stateColor = Color.cyan, entryStateColor = Color.green;

        public State state; //state being represented
        public Vector2 position; //position of box
        [SerializeField]
        private List<SMElementEditor> back, next; //list of element editors before and after this one

        private Rect drawRect;

        //list of booleans saying if inspector hides or show behaviour settings with same idx
        public List<bool> fold = new List<bool>();

        //implementing properties
        public override List<SMElement> NextElem
        {
            get { return state.next; }
        }
        public override List<SMElementEditor> Back { get { return back; } } 
        public override List<SMElementEditor> Next { get { return next; } }

        public override Vector2 Position { get { return position; } }

        //create state editor and its state
        public static StateEditor CreateState(StateMachineEditor sm, Vector2 position)
        {
            StateEditor stateEd = CreateInstance<StateEditor>();
            stateEd.sm = sm;
            stateEd.name = "New State Editor";
            sm.Add(stateEd);
            stateEd.state = CreateInstance<State>();
            AssetDatabase.AddObjectToAsset(stateEd.state, sm.stateMachine);
            stateEd.state.name = "New State";
            stateEd.back = new List<SMElementEditor>();
            stateEd.next = new List<SMElementEditor>();
            stateEd.position = position;
            return stateEd;
        }

        //draw element representation
        public override void Draw()
        {
            GUI.backgroundColor = state == sm.stateMachine.entry ? entryStateColor : stateColor;
            drawRect = new Rect()
            {
                size = size * sm.Zoom,
                center = sm.RealPosToDrawPos(position)
            };
            GUI.Box(drawRect, state.name);
        }
        //get draw order
        public override int DrawOrder() { return 10; }
        //drag state and connected handles by delta
        public override void Drag(Vector2 delta)
        {
            position += delta;
            foreach (Handle h in Back)
                h.Drag(delta);
            foreach (Handle h in Next)
                h.Drag(delta);
        }
        //is mouse position in box
        public override bool IsIn(Vector2 mousePosition)
        {
            return drawRect.Contains(mousePosition);
        }
        //menu to show when right clicked
        public override void Menu(GenericMenu menu)
        {
            menu.AddItem(setEntryGui, false, () => { sm.stateMachine.entry = state; }); //set as sm entry state
            menu.AddItem(deleteGui, false, () => { Delete(); }); //delete this state
        }
        //save state, its editor and behaviours
        public override void Save()
        {
            base.Save();
            EditorUtility.SetDirty(state);

            foreach (StateBehaviour s in state.behaviours)
                EditorUtility.SetDirty(s);
        }


        //add handle that has transition to this state
        public void AddBack(Handle h)
        {
            Back.Add(h);
        }
        protected override void AddBack(SMElementEditor e)
        {
            Handle endHandle = e as Handle;
            if (!endHandle || endHandle.Back == null || endHandle.Next != null)
                return;

            AddBack(endHandle);
        }

        //add handle that has transition out of this state
        public void AddNext(Handle h)
        {
            NextElem.AddRange(h.NextElem);
            Next.Add(h);
        }
        protected override void AddNext(SMElementEditor e)
        {
            Handle startHandle = e as Handle;
            if (!startHandle || startHandle.Back != null || startHandle.Next == null)
                return;

            AddNext(startHandle);
        }

        //remove handle that has transition to this state
        public void RemoveBack(Handle endHandle)
        {
            Back.Remove(endHandle);
        }
        protected override void RemoveBack(SMElementEditor e)
        {
            Handle endHandle = e as Handle;
            if (!endHandle || !Back.Contains(endHandle))
                return;

            RemoveBack(endHandle);
        }

        //remove handle that has transition out of this state
        public void RemoveNext(Handle startHandle)
        {
            NextElem.RemoveAll(x => startHandle.NextElem.Contains(x));
            Next.Remove(startHandle);
        }
        protected override void RemoveNext(SMElementEditor e)
        {
            Handle startHandle = e as Handle;
            if (!startHandle || !Next.Contains(startHandle))
                return;

            RemoveNext(startHandle);
        }

        //delete state and remove it from sm
        private void Delete()
        {
            if (sm.stateMachine.entry == state)
            {
                Debug.LogError("Can't delete entry state, change entry state first to delete this one.");
                return;
            }

            foreach (Handle h in Back)
                h.RemoveNext(this);
            foreach (Handle h in Next)
                h.RemoveBack(this);

            DestroyImmediate(state, true);
            sm.Remove(this);
        }

        //add behabiour of type t to state
        public void AddBehaviour(System.Type t)
        {
            if (!t.IsSubclassOf(typeof(StateBehaviour)))
                return;

            SODatabase.Add(state, (StateBehaviour)CreateInstance(t), state.behaviours);
            fold.Add(true);
        }

        //remove behaviour of index idx
        public void RemoveBehaviour(int idx)
        {
            SODatabase.Remove(state, state.behaviours[idx], state.behaviours);
            fold.RemoveAt(idx);
        }
    }
}