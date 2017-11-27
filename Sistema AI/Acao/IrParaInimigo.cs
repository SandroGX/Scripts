using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class IrParaInimigo : IrPara
    {

        public override void OnAction(SAIController controller)
        {

            destino = controller.inimigo.item.holder.transform.position;

            base.OnAction(controller);
        }

#if UNITY_EDITOR
        public override void GuiParametros()
        {
            base.GuiParametros();
        }
#endif
    }
}
