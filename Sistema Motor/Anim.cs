using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anim
{
    public string paramName;

    public enum VarType { Float, Int, Bool, Trigger };

    public VarType Var;

    public float Float;
    public int Integer;
    public bool Boolean;


    public void SetParam(Animator anim)
    {
        if (paramName != "")
        {
            switch (Var)
            {
                case VarType.Float: anim.SetFloat(paramName, Float); break;
                case VarType.Int: anim.SetInteger(paramName, Integer); break;
                case VarType.Bool: anim.SetBool(paramName, Boolean); break;
                case VarType.Trigger: anim.SetTrigger(paramName); break;

            }
        }
    }

    public void ResetParam(Animator anim)
    {
        if (paramName != "")
        {
            switch (Var)
            {
                case VarType.Float: anim.SetFloat(paramName, 0); break;
                case VarType.Int: anim.SetInteger(paramName, 0); break;
                case VarType.Bool: anim.SetBool(paramName, !Boolean); break;
            }
        }
    }

//#if UNITY_EDITOR
//    public bool mostrar;
//#endif

}
