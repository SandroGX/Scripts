using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using Game.MotorSystem;
using Game.InventorySystem;

[System.Serializable]
public class Character : ItemComponent, IExterior
{
    public DamageableItem damageable;

    public Statistic balance, stamina;
    public bool tired = false;

    public MotorEstado BalanceLost, Fall;
    Motor motor;

    public List<Character> friend = new List<Character>();
    public List<Character> enemy = new List<Character>();


    public override void OnDuplicate()
    {
        damageable = item.GetComponent<DamageableItem>();
        balance = new Statistic(balance);
        stamina = new Statistic(stamina);
        stamina.Max += OnStaminaRecovered;
        stamina.Min += OnStaminaDepleted;   
    }



    public void OnCreate()
    {
        motor = item.holder.gameObject.GetComponent<Motor>();
        item.holder.StartCoroutine(stamina.Variation());
        item.holder.StartCoroutine(balance.Variation());
        damageable.damageable.life.Min += Death;
        damageable.damageable.life.Min += item.holder.Destroy;
    }


    public void OnStaminaDepleted() { tired = true; }
    public void OnStaminaRecovered() { tired = false; }

    public void Death() { Destroy(item); }


#if UNITY_EDITOR

    public override void GuiParameters()
    {
        base.GuiParameters();

        EditorGUILayout.LabelField("Stamina");
        if (stamina != null) stamina.Gui();
        else stamina = new Statistic();
        EditorGUILayout.LabelField("Balance");
        if (balance != null) balance.Gui();
        else balance = new Statistic();

        if (!damageable)
        {
            EditorGUILayout.LabelField("You need a component of type DamageableItem");
            damageable = item.GetComponent<DamageableItem>();
        }
    }

#endif

}
