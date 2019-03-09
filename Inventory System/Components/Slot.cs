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

        public void InsertItem(Item toInsert, out Item toRemove)
        {
            if(CanRemove(inserted) && CanInsert(toInsert))
            {
                if(inserted == Default) toRemove = null;
                else toRemove = inserted;

                if(toInsert) inserted = toInsert;
                else inserted = Default;

                OnInsert(inserted);
                OnWithdraw(toRemove);
            }
            else toRemove = null;
        }


        protected virtual bool CanInsert(Item toInsert) { return true; }
        protected virtual bool CanRemove(Item toRemove) { return true; }

        protected virtual void OnInsert(Item inserted) {    }

        protected virtual void OnWithdraw(Item withdrawn) {     }

        public override void OnDuplicate()
        {
            inserted = Item.Duplicate(inserted ? inserted : Default); 
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
