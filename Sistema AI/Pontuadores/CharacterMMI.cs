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
                case Tipo.danos: aAvaliar = ai.character.danificavel.danificavel.life.value; break;
                case Tipo.danos_Percentagem : aAvaliar = (ai.character.danificavel.danificavel.life.value / ai.character.danificavel.danificavel.life.maxValue) * 100; break;
                case Tipo.stamina: aAvaliar = ai.character.stamina.value; break;
                case Tipo.stamina_Percentagem: aAvaliar = (ai.character.stamina.value / ai.character.stamina.maxValue) * 100; break;
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
