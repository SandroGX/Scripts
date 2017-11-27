using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class DistanciaDoInimigoMMI : MaiorMenorIgual
    {

        public override float Pontuar(SAIController ai)
        {
            if (ai.inimigo)
                aAvaliar = Vector3.Distance(ai.transform.position, ai.inimigo.item.holder.transform.position);
            else return 0;

            return base.Pontuar(ai);
        }

#if UNITY_EDITOR
        protected override void GuiParametros()
        {
            GUILayout.BeginVertical();

            avaliador = EditorGUILayout.FloatField("Distancia: ", avaliador);

            base.GuiParametros();

            GUILayout.EndVertical();
        }
#endif

    }
}
