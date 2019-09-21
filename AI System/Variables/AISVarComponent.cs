using UnityEngine;
using System;
using System.Collections;
using GX.InventorySystem;
using System.Collections.Generic;
using System.Linq;

namespace GX.AISystem
{
    public class AISVarComponent : AISVariable
    {
        Component mb;
        ItemComponent ic;

        public void InitComponent(AISController ctrl)
        {
            mb = ctrl.GetComponent(type);

            if (!mb) ic = ctrl.GetComponent<ItemHolder>().item.GetComponent(type);
        }

        public T GetComponent<T>() where T : Component
        {
            return mb as T;
        }

        public T GetItemComponent<T>() where T : ItemComponent
        {
            return ic as T;
        }

        public override List<string> GetVarTypes()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(Component)) || x == typeof(ItemComponent)).Select(x => x.FullName).ToList();
        }

        protected override string VarType() { return "Component"; }

        public override void Init(AISController ctrl)
        {
            InitComponent(ctrl);
        }

        public override void InspectorGui()
        {
            
        }
    }
}
