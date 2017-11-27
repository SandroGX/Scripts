using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    public class Constante : SAIPontuador
    {
        public float pontuacao;

        public override float Pontuar(SAIController ai)
        {
            return pontuacao;
        }

#if UNITY_EDITOR
        protected override void GuiParametros()
        {
            pontuacao = EditorGUILayout.FloatField("Pontuacao Constante: ", pontuacao);
        }

        public override float GetMaxPontuacao()
        {
            return pontuacao;
        }


        public override float GetMinPontuacao()
        {
            return pontuacao;
        }
#endif
    }
}
