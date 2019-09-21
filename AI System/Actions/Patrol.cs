using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GX.AISystem
{
    [System.Serializable]
    public class Patrol : AISAction
    {

        public AISVarComponent patrolAgent;
        private int patrolIdx;

        public override void OnActionEnter(AISController ctrl)
        {
            base.OnActionEnter(ctrl);

            AISEditorUtil.GetComponent<PatrolAgent>(ctrl, patrolAgent).Subscribe(patrolIdx);
        }

        public override void OnActionExit(AISController ctrl)
        {
            base.OnActionExit(ctrl);

            AISEditorUtil.GetComponent<PatrolAgent>(ctrl, patrolAgent).Unsubscribe(patrolIdx);
        }


#if UNITY_EDITOR
        public override void GuiParameters()
        {
            patrolAgent = (AISVarComponent)AISEditorUtil.VarPopUp("Points", ai, patrolAgent, typeof(AISVarComponent), typeof(PatrolAgent));
            patrolIdx = UnityEditor.EditorGUILayout.IntField("Patrol idx:", patrolIdx);

            base.GuiParameters();
        }
#endif
    }
}
