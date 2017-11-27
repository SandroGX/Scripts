using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anim
{
    public string paramNome;

    public enum p { Float, Int, Bool, Trigger };

    public p Var;

    public float Float;
    public int Integer;
    public bool Boolean;


    public void SetParam(Animator anim)
    {
        if (paramNome != "")
        {
            switch (Var)
            {
                case p.Float: anim.SetFloat(paramNome, Float); break;
                case p.Int: anim.SetInteger(paramNome, Integer); break;
                case p.Bool: anim.SetBool(paramNome, Boolean); break;
                case p.Trigger: anim.SetTrigger(paramNome); break;

            }
        }
    }

#if UNITY_EDITOR
    public bool mostrar;
#endif

}
