using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class DistanciaDoInimigo : SAIPontuador
    {
        public float pontuacao;
        [SerializeField]
        float distanciaDeAval;
        [SerializeField]
        float pontMin, pontMax;

        public override float Pontuar(SAIController ai)
        {
            if (ai.inimigo)
            {
                float dist = Vector3.Distance(ai.inimigo.item.holder.transform.position, ai.transform.position);

                dist = Mathf.Clamp(dist, 0, distanciaDeAval);

                return Mathf.Lerp(pontMin, pontMax, 1 - dist / distanciaDeAval);
            }
            else return 0;
        }

#if UNITY_EDITOR
        protected override void GuiParametros()
        {
            pontuacao = pontMax;

            GUILayout.BeginHorizontal();

            pontMin = EditorGUILayout.FloatField("Pontuacao Min: ", pontMin);
            pontMax = EditorGUILayout.FloatField("Pontuacao Max: ", pontMax);

            GUILayout.EndHorizontal();

            distanciaDeAval = EditorGUILayout.FloatField("Distancia de avaliacao: ", distanciaDeAval);
        }
#endif

    }
}
