using System.Collections;
using System.Collections.Generic;
using Game.InventorySystem;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

[System.Serializable]
public class EquipmentSlot : SlotMotor, IExterior
{
    [SerializeField]
    string parentName;
    Transform parent;
    [SerializeField]
    bool instatiate;

    protected override void SetExterior(Item aPor)
    {
        base.SetExterior(aPor);

        if(aPor && motor)
        {
            Exterior aPorEx = aPor.GetComponent<Exterior>();
            if (aPorEx) aPorEx.Create(parent);
        }
    }

    public override void OnCreate()
    {
        parent = item.holder.GetHolderComponent<Transform>(parentName);

        base.OnCreate();
    }


#if UNITY_EDITOR

    Exterior exterior;

    public override void GuiParameters()
    {
        base.GuiParameters();

        if (exterior)
            Exterior.GetComponentsName<Transform>(exterior, ref parentName);
        else exterior = item.GetComponent<Exterior>();

        instatiate = EditorGUILayout.Toggle("Instatiar exterior se houver?", instatiate);
    }

#endif

}
