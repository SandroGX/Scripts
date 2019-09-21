using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GX.AISystem
{
    public class Constant : AISScorer
    {
        public int constScore;

        public override void StartScoring(AISController ctrl) { }
        public override void StopScoring(AISController ctrl) { }

        public override int GetScore(AISController ai) { return constScore; }

#if UNITY_EDITOR
        protected override void GuiParameters()
        {
            constScore = EditorGUILayout.IntField("Score: ", constScore);
        }


        public override int GetMaxScore() { return constScore; }

        public override int GetMinScore() { return constScore; }
#endif
    }
}
