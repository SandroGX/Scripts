using System.Collections;
using System.Collections.Generic;
using Game.InventorySystem;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

[System.Serializable]
public class SlotEquipamento : SlotMotor, IExterior
{
    [SerializeField]
    string parentName;
    Transform parent;
    [SerializeField]
    bool instatiar;

    protected override void AoPorExterior(Item aPor)
    {
        base.AoPorExterior(aPor);

        if(aPor && motor)
        {
            Exterior aPorEx = aPor.GetComponent<Exterior>();

            if (aPorEx)
            {
                aPorEx.Create(parent);

            }
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

        instatiar = EditorGUILayout.Toggle("Instatiar exterior se houver?", instatiar);
    }

#endif

}
