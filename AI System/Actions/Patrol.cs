using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.AISystem
{
    [System.Serializable]
    public class Patrol : AISAction
    {

        public int pointsIdx = -1;
        Dictionary<AISController, List<Transform>> points = new Dictionary<AISController, List<Transform>>();
        Dictionary<AISController, int> currentPoint = new Dictionary<AISController, int>();

        [SerializeField] float nextPointDist;

        public override void OnActionEnter(AISController ctrl)
        {
            base.OnActionEnter(ctrl);

            List<Transform> pa = AISEditorUtil.GetList<Transform>(ctrl, pointsIdx);
            if (pa == null) return;

            points.Add(ctrl, pa);
            currentPoint.Add(ctrl, 0);

            float disMin = float.MaxValue;
            for(int i = 0; i < pa.Count; i++)
            {
                float dis = Vector3.Distance(ctrl.transform.position, points[ctrl][i].position);
                if (dis < disMin)
                {
                    disMin = dis;
                    currentPoint[ctrl] = i;
                }
            }

            SetDestination(ctrl);
        }

        public override void OnAction(AISController ctrl)
        {
            base.OnAction(ctrl);

            if (!points.ContainsKey(ctrl)) return;

            if (Vector3.Distance(ctrl.transform.position, points[ctrl][currentPoint[ctrl]].position) < nextPointDist)
            {
                currentPoint[ctrl]++;
                if (currentPoint[ctrl] >= points[ctrl].Count) currentPoint[ctrl] = 0;
            }

            SetDestination(ctrl);
        }

        public override void OnActionExit(AISController ctrl)
        {
            base.OnActionExit(ctrl);
            if (!points.ContainsKey(ctrl)) return;
            points.Remove(ctrl);
            currentPoint.Remove(ctrl);
        }

        void SetDestination(AISController ctrl) { ctrl.navAgent.destination = points[ctrl][currentPoint[ctrl]].position; }

#if UNITY_EDITOR
        public override void GuiParameters()
        {
            nextPointDist = UnityEditor.EditorGUILayout.FloatField("Distance to next Point: ", nextPointDist);

            AISEditorUtil.VarPopUp("Points", ai, ref pointsIdx, true, typeof(Transform));

            base.GuiParameters();
        }
#endif
    }
}
