using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 0)]
    [System.Serializable]
    public class Item : ScriptableObject
    {
        //static List<Item> items = new List<Item>();

        public List<ItemComponent> components = new List<ItemComponent>();

        public Item original;
        public ItemHolder holder;

    
        public static Item Duplicate(Item toDuplicate)
        {
            if (!toDuplicate) return null;

            Item duplicated = Instantiate(toDuplicate);

            duplicated.components.Clear();

            if (toDuplicate.original)
                duplicated.original = toDuplicate.original;
            else duplicated.original = toDuplicate;

            duplicated.name = duplicated.original.name;

            foreach (ItemComponent c in toDuplicate.components)
            {
                ItemComponent cd = Instantiate(c);
                duplicated.components.Add(cd);

                cd.name = c.name;
                cd.item = duplicated;
            }

            foreach (ItemComponent cd in duplicated.components)
                cd.OnDuplicate();


            return duplicated;
        }


        public T GetComponent<T>() where T : ItemComponent
        {
            if (components.Count == 0) return null;
            if (components.Count == 1) return components[0] as T;
            IEnumerable<ItemComponent> o = components.Where(x => x is T);//.First() as T;
            if(o.Count() != 0) return o.First() as T;
            return null;
        }

        public ItemComponent GetComponent(System.Type t)
        {
            if (components.Count == 0) return null;
            if (components.Count == 1) return components[0];
            return components.Where(x => x.GetType().Equals(t)).First();
        }

        public ItemComponent GetComponent(string type) { return GetComponent(System.Type.GetType(type)); }

        public T[] GetComponents<T>() where T : ItemComponent
        {
            if (components.Count == 0) return null;
            if (components.Count == 1) return new T[] { components[0] as T };
            return components.Where(x => x is T).Select(x => x as T).ToArray();
        }

        public ItemComponent[] GetComponents(System.Type t)
        {
            if (components.Count == 0) return null;
            return components.Where(x => x.GetType().Equals(t)).ToArray();
        }

        public ItemComponent[] GetComponents(string type)
        {
            if (components.Count == 0) return null;
            System.Type t = System.Type.GetType(type);
            return components.Where(x => x.GetType().Equals(t)).ToArray();
        }


        private void OnDestroy()
        {
            for (int i = 0; i < components.Count; ++i) Destroy(components[i]);
        }



#if UNITY_EDITOR

        static public List<string> allComponents = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().
            Where(x => x.IsSubclassOf(typeof(ItemComponent))).Select(x => x.ToString()).ToList();

        Vector2 scroll = new Vector2();

        public void OnGui()
        {
            GUILayout.BeginScrollView(scroll, false, true);

            name = EditorGUILayout.TextField("Name: ", name);

            for (int i = 0; i < components.Count; i++)
                components[i].OnGui();

            GUILayout.EndScrollView();

        }

#endif 
    }
}
