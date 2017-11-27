using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class StaminaMMI : MaiorMenorIgual
    {

        public override float Pontuar(SAIController ai)
        {
            aAvaliar = ai.character.stamina;

            return base.Pontuar(ai);
        }


#if UNITY_EDITOR
        protected override void GuiParametros()
        {
            GUILayout.BeginVertical();

            avaliador = EditorGUILayout.FloatField("Stamina: ", avaliador);

            base.GuiParametros();

            GUILayout.EndVertical();
        }
#endif
    }
}
