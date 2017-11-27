using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class SeVerdade : SAIPontuador
    {
        [SerializeField]
        float verdade;
        [SerializeField]
        float falso;

        public override float Pontuar(SAIController ai)
        {
            if (Avaliar(ai))
                return verdade;
            else return falso;
        }

        protected virtual bool Avaliar(SAIController ai)
        {
            return true;
        }


#if UNITY_EDITOR
        protected override void GuiParametros()
        {
            GUILayout.BeginHorizontal();

            verdade = EditorGUILayout.FloatField("Se verdade: ", verdade);
            falso = EditorGUILayout.FloatField("Se falso: ", falso);

            GUILayout.EndHorizontal();
        }


        public override float GetMaxPontuacao()
        {
            if (verdade > falso) return verdade; else return falso;
        }


        public override float GetMinPontuacao()
        {
            if (verdade < falso) return verdade; else return falso;
        }
#endif 

    }
}
