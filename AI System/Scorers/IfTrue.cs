using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.AISystem
{
    [System.Serializable]
    public abstract class IfTrue : AISScorer
    {
        [SerializeField]
        protected int trueScore, falseScore;

        public override int CalculateScore(AISController ai)
        {
            if (Evaluate(ai)) return trueScore;
            else return falseScore;
        }

        protected abstract bool Evaluate(AISController ai);

#if UNITY_EDITOR
        protected override void GuiParameters()
        {
            GUILayout.BeginHorizontal();

            trueScore = EditorGUILayout.IntField("If true: ", trueScore);
            falseScore = EditorGUILayout.IntField("If false: ", falseScore);

            GUILayout.EndHorizontal();
        }


        public override int GetMaxScore()
        {
            if (trueScore > falseScore) return trueScore;
            else return falseScore;
        }


        public override int GetMinScore()
        {
            if (trueScore < falseScore) return trueScore;
            else return falseScore;
        }
#endif 

    }
}
