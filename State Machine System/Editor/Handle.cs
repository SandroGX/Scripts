using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace GX.StateMachineSystem
{
    [Serializable]
    public class Handle : SMElementEditor
    {
        private const int maxStateDistance = 10;
        public static readonly int size = 20;
        private static readonly Color handleColor = Color.yellow;

        [SerializeField]
        private Vector2 position;
        [SerializeField]
        private List<SMElement> nextElem;
        [SerializeField]
        private List<SMElementEditor> back, next;

        private Rect drawRect;


        public override Vector2 Position { get => position; }
        public Vector2 DrawPosition { get => drawRect.center; }


        public override List<SMElement> NextElem { get => nextElem; }
        public override List<SMElementEditor> Back { get => back; }
        public override List<SMElementEditor> Next { get => next; }
        public int TransitionCount { get => Back.Count + Next.Count; }

        public StateEditor BackState { get => Back.Count == 1 ? Back[0] as StateEditor : null; }
        public StateEditor NextState { get => Next.Count == 1 ? Next[0] as StateEditor : null; }
        public StateEditor State { get => BackState ? BackState : NextState; }


        public static Handle CreateHandle(StateMachineEditor sm, Vector2 point, List<SMElementEditor> back, List<SMElementEditor> next)
        {
            Handle handle = CreateInstance<Handle>();
            handle.sm = sm;
            sm.Add(handle);
            handle.position = point;
            handle.back = back;
            handle.next = next;
            handle.nextElem = new List<SMElement>();
            handle.name = "Handle";

            return handle;
        }

        public override void Draw()
        {
            GUI.backgroundColor = handleColor;
            drawRect = new Rect
            {
                size = Vector2.one * size * sm.Zoom,
                center = sm.RealPosToDrawPos(position)
            };
            GUI.Box(drawRect, "");
        }
        public override int DrawOrder() { return 20; }
        public override void Drag(Vector2 delta) { position += delta; PutInClosestBorderOrRemove(); }
        public override bool IsIn(Vector2 mousePosition) { return drawRect.Contains(mousePosition); }
        public override void Menu(GenericMenu menu) {  }

        public void PutInClosestBorder()
        {
            GetPointClosestBorder(out Vector2 point, out float dis);

            position = point;
        }
        public void PutInClosestBorderOrRemove()
        {
            GetPointClosestBorder(out Vector2 point, out float dis);

            if (dis < maxStateDistance/sm.Zoom)
                position = point;
            else Separate(State);

        }
        public void GetPointClosestBorder(out Vector2 point, out float dis)
        {
            point = position;
            dis = -1;

            if (!State)
                return;

            Vector2 size2 = StateEditor.size / 2;
            Vector2[] corners = new Vector2[]
            {
                State.position + new Vector2(-size2.x, size2.y),    //top left corner
                State.position + new Vector2(size2.x, size2.y),     //top right corner
                State.position + new Vector2(size2.x, -size2.y),    //bottom right corner
                State.position + new Vector2(-size2.x, -size2.y)    //bottom left corner
            };


            dis = float.PositiveInfinity;
            for (int i = 0; i < 4; ++i)
            {
                Vector2 p = SomeMath.FindNearestPointOnLineSegment(corners[i], corners[(i + 1) % 4], position);
                float d = Vector2.Distance(p, position);
                if (d < dis)
                {
                    point = p;
                    dis = d;
                }
            }

            position = point;
        }

        public void AddBack(TransitionEditor t)
        {
            if (!BackState)
            {
                Back.Add(t);
                t.EndHandle = this;
            }
        }
        public void AddBack(StateEditor s)
        {
            if (!HasBackTransitions())
                Back.Add(s);
        }
        protected override void AddBack(SMElementEditor e)
        {
            TransitionEditor t = e as TransitionEditor;
            if (t)
                AddBack(t);
            StateEditor s = e as StateEditor;
            if (s)
                AddBack(s);
        }

        public void AddNext(TransitionEditor t)
        {
            if (!NextState)
            {
                NextElem.AddRange(t.transitions);
                Next.Add(t);
                t.StartHandle = this;
            }
        }
        public void AddNext(StateEditor s)
        {
            if (!HasNextTransitions())
            {
                NextElem.Add(s.state);
                Next.Add(s);
            }
        }
        protected override void AddNext(SMElementEditor e)
        {
            TransitionEditor t = e as TransitionEditor;
            if (t)
                AddNext(t);
            StateEditor s = e as StateEditor;
            if (s)
                AddNext(s);
        }

        public void RemoveBack(TransitionEditor t)
        {
            if (!BackState)
                Back.Remove(t);
        }
        public void RemoveBack(StateEditor s)
        {
            if (BackState == s)
                Back.Remove(s);
        }
        protected override void RemoveBack(SMElementEditor e)
        {
            TransitionEditor t = e as TransitionEditor;
            if (t)
                RemoveBack(t);
            StateEditor s = e as StateEditor;
            if (s)
                RemoveBack(s);
        }

        public void RemoveNext(TransitionEditor t)
        {
            if (!NextState)
            {
                NextElem.RemoveAll(x => t.transitions.Contains((Transition)x));
                Next.Remove(t);
            }
        }
        public void RemoveNext(StateEditor s)
        {
            if (NextState == s)
            {
                NextElem.Remove(s.state);
                Next.Remove(s);
            }
        }
        protected override void RemoveNext(SMElementEditor e)
        {
            TransitionEditor t = e as TransitionEditor;
            if (t)
                RemoveNext(t);
            StateEditor s = e as StateEditor;
            if (s)
                RemoveNext(s);
        }
        
        public bool HasBackTransitions() { return back.Count != 0 && back[0] is TransitionEditor; }
        public bool HasNextTransitions() { return next.Count != 0 && next[0] is TransitionEditor; }
        public bool HasTransitions() { return HasBackTransitions() || HasNextTransitions(); }

        public static Handle Merge(Handle h1, Handle h2)
        {
            if (h1.State || h2.State || h1.sm != h2.sm)
                return null;

            h1.AddRangeBack(h2.Back);
            h1.AddRangeNext(h2.Next);

            h1.sm.Remove(h2);

            return h1;
        }

        public void Join(Handle h)
        {
            Merge(this, h);
        }
        public void Join(StateEditor s)
        {
            if (State)
                return;

            if (!BackState && !HasBackTransitions())
            {
                AddBack(s);
                s.AddNext(this);
                PutInClosestBorder();
            }
            else if (!NextState && !HasNextTransitions())
            {
                AddNext(s);
                s.AddBack(this);
                PutInClosestBorder();
            }
        }
        public void Join(SMElementEditor e)
        {
            Handle h = e as Handle;
            if (h)
            {
                Join(h);
                return;
            }
            StateEditor s = e as StateEditor;
            if (s)
                Join(s);
        }

        public void Separate(StateEditor s)
        {
            if(s == BackState)
            {
                RemoveBack(s);
                s.RemoveNext(this);
            }
            else if(s == NextState)
            {
                RemoveNext(s);
                s.RemoveBack(this);
            }
        }

    }
}