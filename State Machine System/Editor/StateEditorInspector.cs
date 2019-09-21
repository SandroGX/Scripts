using UnityEngine;
using UnityEditor;

namespace GX.StateMachineSystem
{
    //how to show state in inspector
    [CustomEditor(typeof(StateEditor))]
    public class StateEditorInspector : Editor
    {
        //state to edit
        StateEditor stateE;
        //rect of button "Add", to spawn popup window in the right place
        Rect buttonRect;

        private void OnEnable()
        {
            stateE = (StateEditor)target;
        }

        public override void OnInspectorGUI()
        {
            State state = stateE.state;
            state.name = EditorGUILayout.DelayedTextField("Name", state.name); //name field
            stateE.name = state.name + " Editor";

            //show behaviours settings (if not hidden)
            for (int i = 0; i < state.behaviours.Count; ++i)
            {
                if (stateE.fold[i] = EditorGUILayout.Foldout(stateE.fold[i], state.behaviours[i].GetType().Name))
                {
                    CreateEditor(state.behaviours[i]).OnInspectorGUI();
                    if (GUILayout.Button("Remove"))
                        stateE.RemoveBehaviour(i);
                }
            }

            //show add behaviour popup
            if (GUILayout.Button("Add"))
                PopupWindow.Show(buttonRect, new AddBehaviourPopup(stateE));
            if (Event.current.type == EventType.Repaint)
                buttonRect = GUILayoutUtility.GetLastRect();
        }
    }
}