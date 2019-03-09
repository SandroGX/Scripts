using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.InventorySystem
{
    public abstract class ItemComponentInterface : MonoBehaviour
    {
        public abstract void ConnectItem(Item item);
        public abstract void DisconnectItem();
    }

    public abstract class ItemComponentInterface<C> : ItemComponentInterface where C : ItemComponent
    {
        //private int compIdx;
        protected C itemComponent;

        public override void ConnectItem(Item item) { itemComponent = item.GetComponent<C>(); }

        public override void DisconnectItem() { itemComponent = null; }
    }
}
