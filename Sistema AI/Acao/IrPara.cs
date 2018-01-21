using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class IrPara : MudarEstado
    {
        [SerializeField]
        protected float distanciaStop;

        protected Vector3 destino;

        public override void OnAction(SAIController controller)
        {
            controller.navAgent.stoppingDistance = distanciaStop;
            controller.navAgent.destination = destino;

            base.OnAction(controller);
        }

#if UNITY_EDITOR

        public override void GuiParametros()
        {
            base.GuiParametros();

            distanciaStop = EditorGUILayout.FloatField("Distancia parar:", distanciaStop);
        }
#endif

    }
}
