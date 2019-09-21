using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem
{
    public class StateMachineEditorWindow : EditorWindow
    {
        private static readonly Color backgroundColor = Color.grey, fieldColor = Color.white;
        private static readonly Color gridColor = Color.black;
        private static readonly int gridSpace = 20;
        private static readonly float lineWidth = 1.1f;


        private static readonly Vector2 size = new Vector2(700, 500); //default window size
        private static readonly float scrollwheelMultiplier = 0.01f;

        private StateMachineEditor sm; //selected sm
        private SMElementEditor selected;

        private Vector2 dragOffset;

        [MenuItem("Window/State Machine Window")]
        public static void Init()
        {
            StateMachineEditorWindow win = CreateInstance<StateMachineEditorWindow>();
            win.minSize = size;
            win.Show();
        }

        //to make ongui be called faster
        void Update()
        {
            Repaint();
        }


        private void OnEnable()
        {
            
        }
        private void OnGUI()
        {
            GUILayout.BeginVertical();

            DrawBackground();

            if (sm)
            {
                GUI.backgroundColor = Color.cyan;
                sm.drawCenter = new Vector2(Screen.width / 2, Screen.height / 2);
                DrawSMDiagram();
                ProcessEvents();
            }

            GUILayout.BeginHorizontal();
            SMField();
            Save();
            GUILayout.EndHorizontal();
           
            GUILayout.EndVertical();
        }

        private void DrawBackground()
        {
            //paint window background cheat
            GUI.backgroundColor = backgroundColor;
            GUI.Box(new Rect(Vector2.zero, new Vector2(Screen.width, Screen.height)), "");

            //draw grid
            if (!sm)
                return;

            DrawGrid(gridSpace, lineWidth);
            DrawGrid(gridSpace * 10, lineWidth * 3);
        }
        private void DrawSMDiagram()
        {
            foreach (SMElementEditor s in sm.elems)
                s.Draw();
        }
        private void ProcessEvents()
        {
            Event e = Event.current;
            switch (e.type)
            {
                case EventType.ContextClick:
                    Menu(e);
                    break;
                case EventType.MouseDown:
                    selected = sm.GetSelected(e.mousePosition, null);
                    if(selected)
                        dragOffset = selected.Position - sm.DrawPosToRealPos(e.mousePosition);
                    else dragOffset = sm.DrawPosToRealPos(e.mousePosition);
                    break;
                case EventType.MouseUp:
                    if (!selected)
                        return;
                    Handle h = selected as Handle;
                    if (h)
                        h.Join(sm.GetSelected(sm.RealPosToDrawPos(h.Position), h));
                    else Selection.activeObject = selected;
                    selected = null;
                    break;
                case EventType.MouseDrag:
                    if (selected)
                    {
                        Vector2 p = sm.DrawPosToRealPos(e.mousePosition) + dragOffset;
                        selected.Drag(p - selected.Position);
                    }
                    else
                    {
                        Vector2 o = sm.DrawPosToRealPos(e.mousePosition) - dragOffset;
                        sm.drawOffset += o;
                    }
                        break;
                case EventType.ScrollWheel:
                    sm.Zoom += e.delta.y * scrollwheelMultiplier;
                    break;
            }
        }
        private void Menu(Event e)
        {
            GenericMenu menu = new GenericMenu();

            SMElementEditor elem = sm.GetSelected(e.mousePosition, null);
            if (elem)
                elem.Menu(menu);
            else
            {
                menu.AddItem(new GUIContent("Add State"), false, () => { StateEditor s = StateEditor.CreateState(sm, sm.DrawPosToRealPos(e.mousePosition)); });
                menu.AddItem(new GUIContent("Add Transition"), false, () => { TransitionEditor s = TransitionEditor.CreateTransition(sm, sm.DrawPosToRealPos(e.mousePosition)); });
            }

            menu.ShowAsContext();
        }
        private void SMField()
        {

            GUI.backgroundColor = fieldColor;
            StateMachine smo = (StateMachine)EditorGUILayout.ObjectField("SM", sm ? sm.stateMachine : null, typeof(StateMachine), false);

            if (smo && (!sm || smo != sm.stateMachine))
            {
                sm = null;

                foreach (var sme in Resources.FindObjectsOfTypeAll<StateMachineEditor>())
                {
                    if (sme.stateMachine == smo)
                    { sm = sme; break; }
                }

                if(sm == null)
                    sm = StateMachineEditor.CreateEditor(smo);
            }

        }
        private void Save()
        {
            if (sm && GUILayout.Button("Save"))
                sm.Save();
        }

        private void DrawGrid(int gridSpacing, float lineWidth)
        {
            Vector2 center = sm.DrawPosToRealPos(new Vector2(Screen.width / 2, Screen.height / 2)); //get center of screen in real coordinates

            Vector2 s = new Vector2(Screen.width, Screen.height);
            Vector2 drawSize = s / sm.Zoom;

            DrawGridLines(center, sm.drawOffset.x, Vector2.up, s.y / 2, Vector2.right, drawSize.x, gridSpacing, lineWidth);
            DrawGridLines(center, sm.drawOffset.y, Vector2.right, s.x / 2, Vector2.up, drawSize.y, gridSpacing, lineWidth);
        }
        private void DrawGridLines(Vector2 center, float offset, Vector2 lineDir, float lineDrawLength, Vector2 dir, float drawSize, int gridSpacing, float lineWidth)
        {
            float o = offset % gridSpacing;
            float lineLength = lineDrawLength / sm.Zoom;

            Vector2 s = center + dir * o;
            Vector2 e0 = s + lineDir * lineLength,
                    e1 = s + -lineDir * lineLength,
                    e2 = s - lineDir * lineLength,
                    e3 = s - -lineDir * lineLength;

            float drawSpace = gridSpacing * sm.Zoom;
            int amount = (int)(drawSize / drawSpace);

            DrawParallelLines(sm.RealPosToDrawPos(s), sm.RealPosToDrawPos(e0), dir, amount, drawSpace, lineWidth);
            DrawParallelLines(sm.RealPosToDrawPos(s), sm.RealPosToDrawPos(e1), dir, amount, drawSpace, lineWidth);
            DrawParallelLines(sm.RealPosToDrawPos(s), sm.RealPosToDrawPos(e2), -dir, amount, drawSpace, lineWidth);
            DrawParallelLines(sm.RealPosToDrawPos(s), sm.RealPosToDrawPos(e3), -dir, amount, drawSpace, lineWidth);
        }
        private void DrawParallelLines(Vector2 firstLineStart, Vector2 firstLineEnd, Vector2 dir, int amount, float spacing, float lineWidth)
        {
            for (int i = 0; i < amount; ++i)
            {
                Vector2 off = dir * spacing * i;
                TransitionEditor.DrawLine(firstLineStart + off, firstLineEnd + off, gridColor, lineWidth);
            }
        }
    }
}
