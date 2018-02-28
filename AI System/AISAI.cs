﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Game.AISystem
{
    [System.Serializable]
    public class AISAI : ScriptableObject
    {
        public AISAction root;
        public List<AISVariable> variables = new List<AISVariable>();   

        public AISVarSingle GetSingle(int idx) { return variables[idx] as AISVarSingle; }
        public AISVarList GetList(int idx) { return variables[idx] as AISVarList; }
    }
}
