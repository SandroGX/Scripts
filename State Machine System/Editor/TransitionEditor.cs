using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace GX.StateMachineSystem
{
    //editor for one or more transitions(with same start and end) in state machine
    public class TransitionEditor : SMElementEditor
    {
        //initial distance between handles of transition
        const int initialHandleDistance = 50;
        //max distance to line to IsIn return true
        const int IsInDistance = 4;
        //distance between arrow heads
        const int arrowHeadDist = 10;
        //line width
        const float lineWidth = 3;
        //line color
        private static Color lineColor = Color.white;
        //arrow size
        static Vector2 arrowSize = new Vector2(10, 20);
        //
        private static readonly GUIContent separateStartGui = new GUIContent("Separate Start"),
            separateEndGui = new GUIContent("Separate End"), deleteGui = new GUIContent("Delete");


        //list of booleans saying if inspector hides or show transition settings with same idx
        public List<bool> fold = new List<bool>();
        //list of list of booleans saying if inspector hides or show condition settings with same transition and condition idx
        public List<BooleanListWrapper> foldCondition = new List<BooleanListWrapper>();


        //handles of transition
        [SerializeField]
        private Handle startHandle, endHandle;
        //list of transitions in this transition editor
        public List<Transition> transitions;

        public Handle StartHandle
        {
            get => startHandle;
            set => startHandle = value;
        }
        public Handle EndHandle
        {
            get => endHandle;
            set
            {
                endHandle = value;
                
                //set next in all transitions when handle is changed
                foreach (Transition t in transitions) 
                    t.next = endHandle.NextElem;
            }
        }

        //implementing properties
        public override List<SMElement> NextElem { get => endHandle.NextElem; }
        public override List<SMElementEditor> Back { get => StartHandle.Back; }
        public override List<SMElementEditor> Next { get => EndHandle.Next; }

        //doesnt have a defined position
        public override Vector2 Position { get => (StartHandle.Position + EndHandle.Position) / 2; }

        //create transition between already existing handles, one handle and create other in position, or from one position and create handles 
        public static TransitionEditor CreateTransition(StateMachineEditor sm, Handle start, Handle end)
        {
            TransitionEditor transition = CreateInstance<TransitionEditor>();
            transition.sm = sm;
            sm.Add(transition);
            transition.StartHandle = start;
            transition.transitions = new List<Transition>();
            transition.AddTransition();
            transition.EndHandle = end;
            transition.name = "Transition";

            return transition;
        }
        public static TransitionEditor CreateTransition(StateMachineEditor sm, Handle start, Vector2 end)
        {
            List<SMElementEditor> back = new List<SMElementEditor>();
            TransitionEditor t = CreateTransition(sm, start, Handle.CreateHandle(sm, end, back, new List<SMElementEditor>()));
            back.Add(t);
            return t;
        }
        public static TransitionEditor CreateTransition(StateMachineEditor sm, Vector2 start, Handle end)
        {
            List<SMElementEditor> next = new List<SMElementEditor>();
            TransitionEditor t = CreateTransition(sm, Handle.CreateHandle(sm, start, new List<SMElementEditor>(), next), end);
            next.Add(t);
            return t;
        }
        public static TransitionEditor CreateTransition(StateMachineEditor sm, Vector2 start, Vector2 end)
        {
            List<SMElementEditor> back = new List<SMElementEditor>();
            List<SMElementEditor> next = new List<SMElementEditor>();
            TransitionEditor t = CreateTransition(sm, Handle.CreateHandle(sm, start, new List<SMElementEditor>(), next), Handle.CreateHandle(sm, end, back, new List<SMElementEditor>()));
            back.Add(t);
            next.Add(t);
            return t;
        }
        public static TransitionEditor CreateTransition(StateMachineEditor sm, Vector2 start)
        {
            return CreateTransition(sm, start, start+Vector2.up*initialHandleDistance);
        }

        //draws line between handles with arrow in the middle
        //display 3 arrows when multiple transitions
        public override void Draw()
        {
            Vector2 startPoint = startHandle.DrawPosition;
            Vector2 endPoint = endHandle.DrawPosition;
            Vector2 middlePoint = (startPoint + endPoint) / 2;

            //draw line
            DrawLine(startPoint, endPoint, lineColor, lineWidth);

            //draw arrow
            //TODO: give these proper names
            Vector2 y = (startPoint - endPoint).normalized * arrowSize.y * sm.Zoom;
            Vector2 x = Vector2.Perpendicular(y).normalized * arrowSize.x * sm.Zoom;

            DrawArrowHead(middlePoint, x, y);
            if(transitions.Count > 1)
            {
                Vector2 n = (endPoint - startPoint).normalized * arrowHeadDist * sm.Zoom;
                DrawArrowHead(middlePoint + n, x, y);
                DrawArrowHead(middlePoint - n, x, y);
            }
        }
        //get draw order
        public override int DrawOrder() { return 0; }
        //drag handles by delta and therefore transition
        public override void Drag(Vector2 delta)
        {
            startHandle.Drag(delta);
            endHandle.Drag(delta);
        }
        //is mouse position in transition line
        public override bool IsIn(Vector2 mousePosition)
        {
            return //!startHandle.IsIn(mousePosition) && !endHandle.IsIn(mousePosition) && 
                IsInDistance >= (int)SomeMath.DistBetweenLineSegmentPoint(startHandle.DrawPosition, endHandle.DrawPosition, mousePosition);
        }
        //menu to show when right clicked
        public override void Menu(GenericMenu menu)
        {
            if (!StartHandle.State && StartHandle.TransitionCount != 1)
                menu.AddItem(separateStartGui, false, () => { SeparateStart(); }); //separate transition from start handle(if shared by more than transition)
            if (!EndHandle.State && EndHandle.TransitionCount != 1)
                menu.AddItem(separateEndGui, false, () => { SeparateEnd(); }); //separate transition from start handle(if shared by more than transition)

            menu.AddItem(deleteGui, false, () => { Delete(); } ); //delete this transition
        }
        //save editor, transitions and their conditions
        public override void Save()
        {
            base.Save();
            foreach (Transition t in transitions)
            {
                EditorUtility.SetDirty(t);
                foreach (TransitionCondition c in t.conditions)
                    EditorUtility.SetDirty(c);
            }
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            Handles.DrawBezier(start, end, start, end, color, null, width);
        }
        private void DrawArrowHead(Vector2 position, Vector2 x, Vector2 y)
        {
            DrawLine(position, position + y + x, lineColor, lineWidth);
            DrawLine(position, position + y - x, lineColor, lineWidth);
        }

        //not implemented, handled by handles
        protected override void AddBack(SMElementEditor e)
        {
            throw new System.NotImplementedException();
        }
        protected override void AddNext(SMElementEditor e)
        {
            throw new System.NotImplementedException();
        }
        protected override void RemoveBack(SMElementEditor e)
        {
            throw new System.NotImplementedException();
        }
        protected override void RemoveNext(SMElementEditor e)
        {
            throw new System.NotImplementedException();
        }

        //separate transition from start handle and substitute with new one
        private void SeparateStart()
        {
            StartHandle.RemoveNext(this); //remove this transition from handle
            Vector2 point = (EndHandle.Position - StartHandle.Position).normalized * initialHandleDistance + StartHandle.Position; //get position no far away from other handle

            if (!StartHandle.HasTransitions()) //if handle has no more transitions remove from state machine
                sm.Remove(StartHandle);

            StartHandle = Handle.CreateHandle(sm, point, new List<SMElementEditor>(), new List<SMElementEditor>() { this });
        }
        //separate transition from end handle and substitute with new one
        private void SeparateEnd()
        {
            EndHandle.RemoveBack(this); //remove this transition from handle
            Vector2 point = (StartHandle.Position - EndHandle.Position).normalized * initialHandleDistance + EndHandle.Position; //get point no far away from other handle

            if (!EndHandle.HasTransitions()) //if handle has no more transitions remove from state machine

                sm.Remove(EndHandle);

            EndHandle = Handle.CreateHandle(sm, point, new List<SMElementEditor>() { this }, new List<SMElementEditor>());
        }
        ////delete state and remove it from sm
        private void Delete()
        {
            StartHandle.RemoveNext(this);
            EndHandle.RemoveBack(this);
            sm.Remove(this);

            if (!StartHandle.HasTransitions())
                sm.Remove(StartHandle);
            if (!EndHandle.HasTransitions())
                sm.Remove(EndHandle);
        }

        //adds transition to transition editor
        public void AddTransition()
        {
            Transition t = CreateInstance<Transition>();
            if (EndHandle != null) //during initialization
                t.next = NextElem;
            transitions.Add(t);
            AssetDatabase.AddObjectToAsset(t, sm.stateMachine);

            StartHandle.NextElem.Add(t);
            if (StartHandle.BackState)
                StartHandle.BackState.NextElem.Add(t);

            fold.Add(true);
            foldCondition.Add(new BooleanListWrapper());
        }

        //removes transition from transition editor
        public void RemoveTransition(int idx)
        {
            if(transitions.Count == 1)
            {
                Debug.LogError("Can't remove last transition");
                return;
            }
            Transition t = transitions[idx];
            StartHandle.NextElem.Remove(t);
            if (StartHandle.BackState)
                StartHandle.BackState.NextElem.Remove(t);

            transitions.RemoveAt(idx);
            DestroyImmediate(t, true);

            fold.RemoveAt(idx);
            foldCondition.RemoveAt(idx);
        }

        //add condition to transition
        public void AddCondition(int transitionIdx, System.Type t)
        {
            if (!t.IsSubclassOf(typeof(TransitionCondition)))
                return;

            SODatabase.Add(transitions[transitionIdx], (TransitionCondition)CreateInstance(t), transitions[transitionIdx].conditions);
            foldCondition[transitionIdx].list.Add(true);
        }

        //remove condition of index idx
        public void RemoveCondition(int transitionIdx, int idx)
        {
            SODatabase.Remove(transitions[transitionIdx], transitions[transitionIdx].conditions[idx], transitions[transitionIdx].conditions);
            foldCondition[transitionIdx].list.RemoveAt(idx);
        }


        //because serialization
        [System.Serializable]
        public class BooleanListWrapper
        {
            public List<bool> list = new List<bool>();
        }
    }
}