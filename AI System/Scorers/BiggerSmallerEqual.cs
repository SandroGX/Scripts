using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GX.AISystem
{
    [System.Serializable]
    public abstract class BiggerSmallerEqual : IfTrue
    {

        enum Comparison { BiggerEx, BiggerIn, SmallerEx, SmallerIn, Equal, BetweenEx, BetweenIn, LinearInterp, LinearInterpUnclamp };
        [SerializeField]
        Comparison comp;

        protected float toCompare;
        [SerializeField] protected float comparer, comparer2; //min, max

        public override int CalculateScore(AISController ctrl)
        {
            if (comp <= Comparison.BetweenIn) return base.CalculateScore(ctrl);

            float t = toCompare - comparer / comparer2 - comparer;
            switch (comp)
            {
                case Comparison.LinearInterp: return (int)Mathf.Lerp(falseScore, trueScore, t);
                default:
                case Comparison.LinearInterpUnclamp: return (int)Mathf.LerpUnclamped(falseScore, trueScore, t);
            }
        }


        protected override bool Evaluate(AISController ctrl)
        {
            switch (comp)
            {
                case Comparison.BiggerEx:   return toCompare > comparer;
                case Comparison.BiggerIn:   return toCompare >= comparer;
                case Comparison.SmallerEx:  return toCompare < comparer;
                case Comparison.SmallerIn:  return toCompare <= comparer;
                case Comparison.Equal:      return toCompare == comparer;
                case Comparison.BetweenEx:  return toCompare > comparer && toCompare < comparer2;
                case Comparison.BetweenIn:  return toCompare >= comparer && toCompare <= comparer2;
            }

            return false;
        }


#if UNITY_EDITOR
        protected string toCompName;

        protected override void GuiParameters()
        {
            comp = (Comparison)EditorGUILayout.EnumPopup("Comparison Type ", comp);

            if (comp <= Comparison.Equal) comparer = EditorGUILayout.FloatField(toCompName, comparer);
            else
            {
                comparer = EditorGUILayout.FloatField("Min" + toCompName, comparer);
                comparer2 = EditorGUILayout.FloatField("Max" + toCompName, comparer2);
            }

            if (comp <= Comparison.BetweenIn) base.GuiParameters();
            else
            {
                falseScore = EditorGUILayout.IntField("Min Score", falseScore);
                trueScore = EditorGUILayout.IntField("Max Score", trueScore);
            }
        }
#endif
    }
}
