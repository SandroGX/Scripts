using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.InventorySystem
{
    [AddComponentMenu("Inventario/Item Instatiator")]
    public class ItemInstatiator : MonoBehaviour
    {
        public Item original;

        void Awake()
        {
            Create(original);
        }

        void Create(Item item)
        {
            item.GetComponent<Exterior>().Create(transform.position, transform.rotation);
        }
    }
}
