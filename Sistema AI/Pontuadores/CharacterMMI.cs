using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    public class CharacterMMI : MaiorMenorIgual
    {

        enum Tipo { danos, danos_Percentagem, stamina, stamina_Percentagem}
        [SerializeField]
        Tipo tipo;

        public override float Pontuar(SAIController ai)
        {
            switch (tipo)
            {
                case Tipo.danos: aAvaliar = ai.character.danificavel.danos; break;
                case Tipo.danos_Percentagem : aAvaliar = ((float)ai.character.danificavel.danos / (float)ai.character.danificavel.danosMax) * 100; break;
                case Tipo.stamina: aAvaliar = ai.character.stamina; break;
                case Tipo.stamina_Percentagem: aAvaliar = ((float)ai.character.stamina / (float)ai.character.staminaMax) * 100; break;
            }

            return base.Pontuar(ai);
        }



#if UNITY_EDITOR

        string a;

        protected override void GuiParametros()
        {
            GUILayout.BeginVertical();

            tipo = (Tipo)EditorGUILayout.EnumPopup("Como avaliar: ", tipo);
            avaliador = EditorGUILayout.FloatField(tipo.ToString() + ": ", avaliador);

            base.GuiParametros();

            GUILayout.EndVertical();
        }
#endif
    }
}
