using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class MaiorMenorIgual : SeVerdade
    {

        enum Avaliacao { Maior, Menor, Igual};
        [SerializeField]
        Avaliacao t;

        protected float aAvaliar;
        [SerializeField]
        protected float avaliador;

        public override float Pontuar(SAIController ai)
        {
            return base.Pontuar(ai);
        }


        protected override bool Avaliar(SAIController ai)
        {
            switch (t)
            {
                case Avaliacao.Maior:
                    if (aAvaliar > avaliador)
                        return true;
                    break;

                case Avaliacao.Menor:
                    if (aAvaliar < avaliador)
                        return true;
                    break;

                case Avaliacao.Igual:
                    if (aAvaliar == avaliador)
                        return true;
                    break;
            }

            return false;
        }


#if UNITY_EDITOR
        protected override void GuiParametros()
        {
            GUILayout.BeginVertical();

            t = (Avaliacao)EditorGUILayout.EnumPopup("Verdade se: ", t);

            base.GuiParametros();

            GUILayout.EndVertical();
        }
#endif
    }
}
