using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class ManterDistanciaInimigo :  IrPara
    {
        [SerializeField]
        float distanciaAManter;

        public override void OnAction(SAIController controller)
        {
            Vector3 dir = Vector3.Normalize(controller.transform.position - controller.inimigo.item.holder.transform.position);

            destino = controller.inimigo.item.holder.transform.position + dir * distanciaAManter;

            Debug.DrawLine(destino, destino + Vector3.up * 2, Color.red);

            base.OnAction(controller);
        }

#if UNITY_EDITOR
        public override void GuiParametros()
        {
            distanciaAManter = EditorGUILayout.FloatField("Distancia a Manter", distanciaAManter);

            base.GuiParametros();
        }
#endif
    }
}
