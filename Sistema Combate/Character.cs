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
    public DanificavelItem danificavel;

    public Statistic unbalance, stamina;

    public MotorEstado PerdaDeEquilibrio, Cair;
    Motor motor;

    public List<Character> amigo = new List<Character>();
    public List<Character> inimigo = new List<Character>();


    public override void AoDuplicar()
    {
        danificavel = item.GetComponent<DanificavelItem>();
        unbalance = new Statistic(unbalance);
        stamina = new Statistic(stamina);
    }



    public void OnCriado()
    {
        motor = item.holder.gameObject.GetComponent<Motor>();
        stamina = new Statistic(stamina);
        unbalance = new Statistic(unbalance);
        item.holder.StartCoroutine(stamina.Variation());
        item.holder.StartCoroutine(unbalance.Variation());
    }


#if UNITY_EDITOR

    public override void GuiParametros()
    {
        base.GuiParametros();

        EditorGUILayout.LabelField("Stamina");
        stamina.Gui();
        EditorGUILayout.LabelField("Unbalance");
        unbalance.Gui();

        if (!danificavel)
        {
            EditorGUILayout.LabelField("Precisa de um componente do tipo DanificavelItem");
            danificavel = item.GetComponent<DanificavelItem>();
        }
    }

#endif

}
