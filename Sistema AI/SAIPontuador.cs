using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.SistemaAI
{
    [System.Serializable]
    public class SAIPontuador : ScriptableObject
    {
        //public float pontuacao;

        public SAIAI ai;
        public SAIAcao dono;

        public virtual float Pontuar(SAIController ai) { return 0; }

#if UNITY_EDITOR

        bool mostrar;

        public void OnGui()
        {
            GUILayout.BeginHorizontal();

            mostrar = EditorGUILayout.Foldout(mostrar, name + ": " + GetMinPontuacao() + " - " + GetMaxPontuacao());

            GUILayout.BeginVertical();

            GUILayout.Space(20);

            if (mostrar)
            {
                name = EditorGUILayout.TextField("Nome: ", name);
                GUILayout.Label(dono.name);
                GuiParametros();
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }


        protected virtual void GuiParametros()
        {
            GUILayout.Label("Sou pontuador");
        }


        public virtual float GetMaxPontuacao()
        {
            return 0;
        }


        public virtual float GetMinPontuacao()
        {
            return 0;
        }

#endif
    }
}
