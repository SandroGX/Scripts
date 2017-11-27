using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class DebugAI : SAIAcao
    {
        [SerializeField]
        string mensagem;

        public override void OnAction(SAIController controller)
        {
            Debug.Log(mensagem);

            base.OnAction(controller);
        }

#if UNITY_EDITOR

        public override void GuiParametros()
        {
            mensagem = EditorGUILayout.TextField("Mensagem: ", mensagem);
        }
#endif
    }
}
