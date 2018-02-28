using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.AISystem
{
    [System.Serializable]
    public class AISScorer : ScriptableObject
    {
        public Dictionary<AISController, int> score = new Dictionary<AISController, int>();
        public Dictionary<AISController, Coroutine> coroutines = new Dictionary<AISController, Coroutine>();
        public AISAI ai;
        public AISAction owner;
        public float updateTime = 0.2f;

        public virtual void StartScoring(AISController ctrl)
        {
            score.Add(ctrl, 0);
            coroutines.Add(ctrl, ctrl.StartCoroutine(ScoreCoroutine(ctrl)));
        }
        public virtual void StopScoring(AISController ctrl)
        {
            score.Remove(ctrl);
            ctrl.StopCoroutine(coroutines[ctrl]);
            coroutines.Remove(ctrl);
        }

        public virtual int CalculateScore(AISController ctrl) { return 0; }
        private void Score(AISController ctrl) { score[ctrl] = CalculateScore(ctrl); }
        IEnumerator ScoreCoroutine(AISController ctrl)
        {
            while (true)
            {
                Score(ctrl);
                yield return new WaitForSeconds(updateTime);
            }
        }

        public virtual int GetScore(AISController ctrl) { return score[ctrl]; }
        public virtual int GetMaxScore() { return 0; }
        public virtual int GetMinScore() { return 0; }

#if UNITY_EDITOR

        bool show;

        public void OnGui()
        {
            show = EditorGUILayout.Foldout(show, name + ": " + GetMinScore() + " - " + GetMaxScore());
            if (show)
            {
                ++EditorGUI.indentLevel;
                name = EditorGUILayout.TextField("Name: ", name);
                updateTime = EditorGUILayout.FloatField("Update Time:", updateTime);
                GuiParameters();
                --EditorGUI.indentLevel;
            }
        }


        protected virtual void GuiParameters()
        {
            GUILayout.Label("I'm scorer");
        }
#endif
    }
}
