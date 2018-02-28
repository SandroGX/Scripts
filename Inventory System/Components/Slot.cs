using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.InventorySystem
{
    [System.Serializable]
    public class Slot : ItemComponent
    {
        public Item inserted;
        public Item Default;


        public override void OnDuplicate()
        {
            if(Default) Default = Item.Duplicate(Default);

            if(inserted) inserted = Item.Duplicate(inserted);

            if (!inserted && Default) inserted = Default;
        }


        public void InsertItem(Item toInsert, out Item toRemove)
        {
            if (Conditions(toInsert))
            {
                if (inserted == Default) toRemove = null;
                else toRemove = inserted;

                if (toInsert) inserted = toInsert;
                else inserted = Default;

                OnInsert(inserted);
                OnWithDraw(toRemove);
            }
            else toRemove = null;
        }


        bool Conditions(Item aAvaliar)
        {
            return true;
        }


        protected virtual void OnInsert(Item toInsert)
        {
             
        }


        protected virtual void OnWithDraw(Item toWithDraw)
        {

        }


#if UNITY_EDITOR

        public override void GuiParameters()
        {
            base.GuiParameters();

            Default = EditorGUILayout.ObjectField("Default: ", Default, typeof(Item), false) as Item;
            inserted = EditorGUILayout.ObjectField("Item: ", inserted, typeof(Item), false) as Item;
        }

#endif
    }
}
