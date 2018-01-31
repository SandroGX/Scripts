using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Game.MotorSystem;

namespace Game.SistemaAI
{
    [System.Serializable]
    public class MudarEstado : SAIAcao
    {
        [SerializeField]
        MotorEstado estado;

        public override void OnActionEnter(SAIController controller)
        {
            controller.motor.nextState = estado;

            base.OnActionEnter(controller);
        }


        public override void OnAction(SAIController controller)
        {
            

            base.OnAction(controller);
        }

#if UNITY_EDITOR
        public override void GuiParametros()
        {
            base.GuiParametros();

            EditorGUILayout.BeginHorizontal();

            estado = EditorGUILayout.ObjectField(estado, typeof(MotorEstado), false) as MotorEstado;
            //esperarAnimAcabar = EditorGUILayout.Toggle("Esperar animacao acabar: ", esperarAnimAcabar);

            EditorGUILayout.EndHorizontal();
        }
#endif

    }
}
