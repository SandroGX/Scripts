using UnityEngine;
using UnityEditor;

namespace GX.StateMachineSystem
{
    //how to show state in inspector
    [CustomEditor(typeof(TransitionEditor))]
    public class TransitionEditorInspector : Editor
    {
        //state to edit
        TransitionEditor transitionEd;
 

        private void OnEnable()
        {
            transitionEd = (TransitionEditor)target;
        }

        public override void OnInspectorGUI()
        {
            transitionEd.name = EditorGUILayout.DelayedTextField("Name", transitionEd.name); //name field

            int toDelete = -1;

            for (int i = 0; i < transitionEd.transitions.Count; ++i)
            {
                EditorGUILayout.BeginHorizontal();

                transitionEd.fold[i] = EditorGUILayout.Foldout(transitionEd.fold[i], "Transition " + i);
                if (GUILayout.Button("Remove Transition"))
                    toDelete = i;

                EditorGUILayout.EndHorizontal();

                if (transitionEd.fold[i])
                {
                    ++EditorGUI.indentLevel;
                    TransitionGUI(i);
                    --EditorGUI.indentLevel;
                }
            }

            if(toDelete >= 0)
                transitionEd.RemoveTransition(toDelete);

            if (GUILayout.Button("Add Transition"))
                transitionEd.AddTransition();
        }

        private void TransitionGUI(int transitionIdx)
        {
            Transition t = transitionEd.transitions[transitionIdx];

            int toDelete = -1;

            //show conditions settings (if not hidden)
            for (int i = 0; i < t.conditions.Count; ++i)
            {
                EditorGUILayout.BeginHorizontal();

                transitionEd.foldCondition[transitionIdx].list[i] =
                    EditorGUILayout.Foldout(transitionEd.foldCondition[transitionIdx].list[i], t.conditions[i].GetType().Name);
                if (GUILayout.Button("X"))
                    toDelete = i;

                EditorGUILayout.EndHorizontal();

                if (transitionEd.foldCondition[transitionIdx].list[i])
                    CreateEditor(t.conditions[i]).OnInspectorGUI(); //show condition inspector

            }

            if (toDelete >= 0)
                transitionEd.RemoveCondition(transitionIdx, toDelete);

            //rect of button "Add Condition", to spawn popup window in the right place
            Rect buttonRect = GUILayoutUtility.GetLastRect();
            //show add behaviour popup
            if (GUILayout.Button("Add Condition"))
                PopupWindow.Show(buttonRect, new AddConditionPopup(transitionEd, transitionIdx));

        }
    }
}