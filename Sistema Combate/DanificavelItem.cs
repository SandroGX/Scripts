using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Game;
using Game.SistemaInventario;

public class DanificavelItem : ItemComponent, IExterior
{
    public Danificavel danificavel;

    [SerializeField]
    List<string> hitboxesName;
    public List<Hitbox> hitboxes = new List<Hitbox>();


    public override void AoDuplicar()
    {
        danificavel = new Danificavel(danificavel);
        hitboxes.Clear();
    }



    public void OnCriado()
    {

        hitboxes = item.holder.GetHolderComponents<Hitbox>(hitboxesName.ToArray());

        foreach (Hitbox h in hitboxes) h.OnHitEnter += ReceberDanos;
        
        item.holder.StartCoroutine(danificavel.life.Variation());

    }



    public void ReceberDanos(HitInfo info)
    {
        danificavel.ReceiveDamage(info.danos);
    }



    void OnDestroy()
    {
        foreach (Hitbox h in hitboxes)
            h.OnHitEnter -= ReceberDanos;

    }


#if UNITY_EDITOR

    Exterior exterior;
    public int size = 1;

    public override void GuiParametros()
    {
        base.GuiParametros();

        if(danificavel) danificavel.Gui();

        if (exterior) Exterior.GetComponentsNames<Hitbox>(exterior, ref size, hitboxesName);
        else exterior = item.GetComponent<Exterior>();

    }
#endif

}
