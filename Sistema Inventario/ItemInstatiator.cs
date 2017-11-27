using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaInventario
{
    [AddComponentMenu("Inventario/Item Instatiator")]
    public class ItemInstatiator : MonoBehaviour
    {
        public Item original;

        void Awake()
        {
            Criar(original);
        }

        void Criar(Item item)
        {
            item.GetComponent<Exterior>().Criar(transform.position, transform.rotation);
        }
    }
}
