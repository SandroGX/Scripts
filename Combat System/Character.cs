using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using GX.MotorSystem;
using GX.InventorySystem;

[System.Serializable]
public class Character : ItemComponent, IExterior
{
    public DamageableItem damageable;

    public Statistic balance, stamina;
    public bool tired = false;

    public List<Character> friend = new List<Character>();
    public List<Character> enemy = new List<Character>();


    public override void OnDuplicate()
    {
        damageable = item.GetComponent<DamageableItem>();
        balance = new Statistic(balance);
        stamina = new Statistic(stamina);
        damageable.Life.Min += Death;
        stamina.Max += OnStaminaRecovered;
        stamina.Min += OnStaminaDepleted;   
    }


    public void OnExteriorConnect()
    {
        item.holder.StartCoroutine(stamina.Variation());
        item.holder.StartCoroutine(balance.Variation());
        damageable.Life.Min += item.holder.Destroy;
    }

    public void OnExteriorDisconnect()
    {
        item.holder.StopCoroutine(stamina.Variation());
        item.holder.StopCoroutine(balance.Variation());
        damageable.Life.Min -= item.holder.Destroy;
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
