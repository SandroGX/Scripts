using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Game;
using Game.InventorySystem;

public class DamageableItem : ItemComponent, IExterior
{
    public Damageable damageable;

    [SerializeField]
    List<string> hitboxesName;
    public List<Hitbox> hitboxes = new List<Hitbox>();

    public override void OnDuplicate()
    {
        damageable = new Damageable(damageable);
        hitboxes.Clear();
    }


    public void OnCreate()
    {

        hitboxes = item.holder.GetHolderComponents<Hitbox>(hitboxesName.ToArray());

        foreach (Hitbox h in hitboxes) h.OnHitEnter += ReceiveDamage;
        
        item.holder.StartCoroutine(damageable.life.Variation());

    }


    public void ReceiveDamage(HitInfo info)
    {
        damageable.ReceiveDamage(info.damage);
    }


    void OnDestroy()
    {
        foreach (Hitbox h in hitboxes)
            h.OnHitEnter -= ReceiveDamage;

    }

#if UNITY_EDITOR
    Exterior exterior;
    public int size = 1;

    public override void GuiParameters()
    {
        base.GuiParameters();

        if (damageable) damageable.Gui();
        else damageable = new Damageable();

        if (exterior) Exterior.GetComponentsNames<Hitbox>(exterior, ref size, hitboxesName);
        else exterior = item.GetComponent<Exterior>();

    }
#endif

}
