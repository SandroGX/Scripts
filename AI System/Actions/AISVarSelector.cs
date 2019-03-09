using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.Linq;

namespace Game.AISystem
{
    public class AISVarSelector : AISAction
    {
        [SerializeField] AISVariable listKey;
        [SerializeField] AISVariable best;
        IAISVarScorer[] varScorers;

        public override void Init()
        {
            varScorers = scorers.Where(x => x as IAISVarScorer != null).Select(x => x as IAISVarScorer).ToArray();
            base.Init();
        }


        public override void OnAction(AISController ctrl)
        {
            base.OnAction(ctrl);

            AISVarList l = (AISVarList)ctrl.GetVar(listKey);
            l.PassToSingle((AISVarSingle)ctrl.GetVar(best), GetBest(ctrl, l));
        }


        int GetBest(AISController ctrl, AISVarList l)
        {
            float maxPoints = float.MinValue;
            int idx = -1;

            for(int i = 0; i < l.@object.Count; ++i)
            {
                float s = Score(ctrl, l, i);
                if(s > maxPoints) { idx = i; maxPoints = s; }
            }

            return idx;
        }

        float Score(AISController ctrl, AISVarList l, int idx)
        {
            float scor = 0;

            foreach (var v in varScorers) scor += v.Score(ctrl, l, idx);

            return scor;
        }

#if UNITY_EDITOR

        public override void GuiParameters()
        {
            listKey = AISEditorUtil.VarPopUp("List to select", ai, listKey, typeof(AISVarList));
            best = AISEditorUtil.VarPopUp("Single to select", ai, best, typeof(AISVarSingle));
        }

#endif

    }
}