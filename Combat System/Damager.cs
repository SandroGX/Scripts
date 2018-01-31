using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Game;
using Game.InventorySystem;

[System.Serializable]
public class Damager : ItemComponent, IExterior, IAtivavel
{
    public string Name { get { return name; } }
    public bool isActive { get; set; }

    public Damage damageToGive;
    public List<string> hitboxesName;
    public List<Hitbox> hitboxes = new List<Hitbox>();



    public void OnCreate()
    {
        hitboxes = item.holder.GetHolderComponents<Hitbox>(hitboxesName.ToArray());
    }



    public override void OnDuplicate()
    {
        hitboxes.Clear();
    }



    public void Activate(bool activate)
    {
        if (activate == isActive) return;

        isActive = activate;

        foreach(Hitbox h in hitboxes)
        {
            if (activate) h.hit.damage += damageToGive;
            else h.hit.damage -= damageToGive;

            h.ActivateHitBox(activate);
        }   
    }


#if UNITY_EDITOR

    Exterior exterior;
    public int size = 1;

    public override void GuiParameters()
    {
        base.GuiParameters();

        damageToGive.Gui();

        if (exterior)
            Exterior.GetComponentsNames<Hitbox>(exterior, ref size, hitboxesName);
        else exterior = item.GetComponent<Exterior>();
        
    }
#endif
}