using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Game;
using Game.SistemaInventario;

[System.Serializable]
public class Danificador : ItemComponent, IExterior, IAtivavel
{
    public string Name { get { return name; } }
    public bool isActive { get; set; }

    public Danos danosADar;
    public List<string> hitboxesName;
    public List<Hitbox> hitboxes = new List<Hitbox>();



    public void OnCriado()
    {
        hitboxes = item.holder.GetHolderComponents<Hitbox>(hitboxesName.ToArray());
    }



    public override void AoDuplicar()
    {
        hitboxes.Clear();
    }



    public void Activate(bool ativo)
    {
        if (ativo == isActive) return;

        isActive = ativo;

        foreach(Hitbox h in hitboxes)
        {
            if (ativo)
                h.hit.danos += danosADar;
            else h.hit.danos -= danosADar;

            h.Ativar(ativo);
        }   
    }


#if UNITY_EDITOR

    Exterior exterior;
    public int size = 1;

    public override void GuiParametros()
    {
        base.GuiParametros();

        danosADar.Gui();

        if (exterior)
            Exterior.GetComponentsNames<Hitbox>(exterior, ref size, hitboxesName);
        else exterior = item.GetComponent<Exterior>();
        
    }
#endif
}