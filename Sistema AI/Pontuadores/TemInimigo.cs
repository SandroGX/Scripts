using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class TemInimigo : SeVerdade
    {
        
        protected override bool Avaliar(SAIController ai)
        {
            if (ai.inimigo)
                return true;
            else return false;
        }

    }
}
