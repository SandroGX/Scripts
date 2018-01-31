using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anim
{
    public string paramNome;

    public enum VarType { Float, Int, Bool, Trigger };

    public VarType Var;

    public float Float;
    public int Integer;
    public bool Boolean;


    public void SetParam(Animator anim)
    {
        if (paramNome != "")
        {
            switch (Var)
            {
                case VarType.Float: anim.SetFloat(paramNome, Float); break;
                case VarType.Int: anim.SetInteger(paramNome, Integer); break;
                case VarType.Bool: anim.SetBool(paramNome, Boolean); break;
                case VarType.Trigger: anim.SetTrigger(paramNome); break;

            }
        }
    }

    public void ResetParam(Animator anim)
    {
        if (paramNome != "")
        {
            switch (Var)
            {
                case VarType.Float: anim.SetFloat(paramNome, 0); break;
                case VarType.Int: anim.SetInteger(paramNome, 0); break;
                case VarType.Bool: anim.SetBool(paramNome, !Boolean); break;
            }
        }
    }

//#if UNITY_EDITOR
//    public bool mostrar;
//#endif

}
