using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Game.SistemaAI
{
    [System.Serializable]
    public class SAIAI : ScriptableObject
    {
        public SAIAcao root;
    }
}
