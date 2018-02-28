using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Game.AISystem
{
    public class AISVarSelector : AISAction
    {
        int list;
        int best;
        IAISVarScorer[] varScorers;

        public override void Init()
        {
            varScorers = scorers.Where(x => x as IAISVarScorer != null).Select(x => x as IAISVarScorer).ToArray();
            base.Init();
        }


        public override void OnAction(AISController controller)
        {
            base.OnAction(controller);

            controller.GetList(list).PassToSingle(controller.GetSingle(best), GetBest(controller));
        }


        int GetBest(AISController controller)
        {
            float maxPoints = float.MinValue;
            int a = -1;
            for(int i = 0; i < controller.GetList(i).@object.Count; ++i)
            {
                float p = Score(controller, i);
                if (p > maxPoints) { a = i; maxPoints = p; }
            }

            return a;
        }

        float Score(AISController controller, int idx)
        {
            float scor = 0;

            foreach (var v in varScorers) scor += v.Score(controller, controller.GetList(idx), idx);

            return scor;
        }

#if UNITY_EDITOR

        public override void GuiParameters()
        {
            AISEditorUtil.VarPopUp("List to select", ai, ref list, true);
            AISEditorUtil.VarPopUp("Single to select", ai, ref best, false);
        }

#endif

    }
}