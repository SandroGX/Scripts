using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.AISystem
{
    [System.Serializable]
    public class GoTo : AISAction
    {
        [SerializeField]
        protected float stopDistance;

        int destinyIdx = -1;
        protected Dictionary<AISController, Transform> destiny = new Dictionary<AISController, Transform>();


        public override void OnActionEnter(AISController ctrl)
        {
            base.OnActionEnter(ctrl);

            Transform t = AISEditorUtil.GetSingle<Transform>(ctrl, destinyIdx);
            if (!t) return;

            destiny.Add(ctrl, t);

            if (!destiny[ctrl]) return;
        }

        public override void OnAction(AISController ctrl)
        {
            base.OnAction(ctrl);

            if (!destiny.ContainsKey(ctrl)) return;

            ctrl.navAgent.stoppingDistance = stopDistance;
            ctrl.navAgent.destination = destiny[ctrl].position;
        }

        public override void OnActionExit(AISController ctrl)
        {
            base.OnActionExit(ctrl);

            if (!destiny.ContainsKey(ctrl)) return;

            destiny.Remove(ctrl);
            ctrl.navAgent.destination = ctrl.transform.position;
        }

#if UNITY_EDITOR

        public override void GuiParameters()
        {
            base.GuiParameters();

            AISEditorUtil.VarPopUp("Destiny:", ai, ref destinyIdx, false, typeof(Transform));

            stopDistance = EditorGUILayout.FloatField("Stop distance:", stopDistance);
        }
#endif

    }
}
