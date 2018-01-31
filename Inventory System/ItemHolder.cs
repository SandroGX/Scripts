using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.InventorySystem
{
    [AddComponentMenu("Inventario/Item Holder")]
    public class ItemHolder : MonoBehaviour
    {

        public Item item;

        public T GetHolderComponent<T>(string componentName) where T : Component
        {
            T t;
            if (name.Contains(componentName)) t = GetComponent<T>();
            else
            {
                T[] d = GetComponentsInChildren<T>();
                t = d.First(x => x.name == componentName);
            }

            return t;
        }


        public List<T> GetHolderComponents<T>(string[] names) where T : Component
        {
            List<T> a = new List<T>();
            T[] d = GetComponentsInChildren<T>();

            foreach (string s in names)
            {
                if (name.Contains(s)) a.Add(GetComponent<T>());
                else a.Add(d.First(x => x.name == s));
            }
            return a;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

    }
}
