using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using Game.SistemaMotor;
using Game.SistemaInventario;

[System.Serializable]
public class Character : ItemComponent, IExterior
{
    public Danificavel danificavel;

    public int desequilibrio, varDesequilibrio, desequilibrioMax, stamina, staminaVar, staminaMax;

    public MotorEstado PerdaDeEquilibrio, Cair;

    Motor motor;
    IEnumerator podeVar;

    public List<Character> amigo = new List<Character>();
    public List<Character> inimigo = new List<Character>();

    public delegate void Morri(Character eu);
    public event Morri morri;



    public override void AoDuplicar()
    {
        danificavel = item.GetComponent<DanificavelItemComponente>().danificavel;
    }



    public void OnCriado()
    {
        motor = item.holder.gameObject.GetComponent<Motor>();
        stamina = staminaMax;
        item.holder.StartCoroutine(Variacao());
    }



    IEnumerator Variacao()
    {
        while (true)
        {
            danificavel.danos += danificavel.varDanos;
            desequilibrio += varDesequilibrio;
            stamina += staminaVar;

            //Corrigir();

            //Morte();

            yield return new WaitForSeconds(1);
        }

    }



    //public override void ReceberDanos(int danosAReceber)
    //{

    //    danos += danosAReceber;

    //    desequilibrio += danosAReceber;

    //    Corrigir();

    //    if (Morte())
    //        return;

    //    if(desequilibrio >= desequilibrioMax)
    //    {
    //        //if (stamina <= staminaMax * .25f || danosAReceber >= desequilibrioMax)
    //            motor.MudarEstado(Cair);
    //        //else motor.MudarEstado(PerdaDeEquilibrio);

    //        desequilibrio = 0;
    //    }

    //}


#if UNITY_EDITOR

    public override void GuiParametros()
    {
        base.GuiParametros();

        staminaVar = EditorGUILayout.IntField("Stamina Var", staminaVar);
        staminaMax = EditorGUILayout.IntField("Stamina Max", staminaMax);

        if (!danificavel)
        {
            danificavel = item.GetComponent<DanificavelItemComponente>().danificavel;
            EditorGUILayout.LabelField("Precisa de um componente do tipo DanificavelItemComponente");
        }
    }

#endif

}
