using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GX.AISystem
{
    [System.Serializable]
    public class GoTo : AISAction
    {
        [SerializeField] protected float stopDistance;

        [SerializeField] AISVariable destinyKey;
        protected Dictionary<AISController, Transform> destiny = new Dictionary<AISController, Transform>();


        public override void OnActionEnter(AISController ctrl)
        {
            base.OnActionEnter(ctrl);

            Transform t = AISEditorUtil.GetSingleObject<Transform>(ctrl, destinyKey);
            if (!t) return;

            destiny.Add(ctrl, t);
        }

        public override void OnAction(AISController ctrl)
        {
            base.OnAction(ctrl);

            if (!destiny.ContainsKey(ctrl) || ctrl.navAgent == null) return;

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

            destinyKey = AISEditorUtil.VarPopUp("Destiny:", ai, destinyKey, typeof(AISVarSingle), typeof(Transform));

            stopDistance = EditorGUILayout.FloatField("Stop distance:", stopDistance);
        }
#endif

    }
}
