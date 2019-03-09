using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.InventorySystem
{
    [System.Serializable]
    public class Exterior : ItemComponent, IExterior
    {
        public GameObject original, exterior;
        [SerializeField]
        Material materialOverride;


        public GameObject Create(Vector3 position)
        {
            GameObject g = Instantiate(original, position, Quaternion.identity);
            ConnectToExterior(g);
            return g;
        }

        public GameObject Create(Vector3 position, Quaternion rotation)
        {
            GameObject g = Instantiate(original, position, rotation);
            ConnectToExterior(g);
            return g;
        }

        public GameObject Create(Transform parent)
        {
            GameObject g = Instantiate(original, parent);
            ConnectToExterior(g);
            return g;
        }

        public GameObject Create(Transform parent, bool instantiateInWorldSpace = false)
        {
            GameObject g = Instantiate(original, parent, instantiateInWorldSpace);
            ConnectToExterior(g);
            return g;
        }

        public GameObject Create(Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject g = Instantiate(original, position, rotation, parent);
            ConnectToExterior(g);
            return g;
        }


        private void ConnectToExterior(GameObject g)
        {

            ItemHolder h = g.AddComponent<ItemHolder>();

            Item i = !item.original ? Item.Duplicate(item) : i = item;

            i.holder = h;
            h.item = i;

            foreach (ItemComponentInterface c in h.GetComponents<ItemComponentInterface>())
                c.ConnectItem(i);

            foreach (ItemComponent c in i.components)
            {
                IExterior e = c as IExterior;
                if(e != null) e.OnExteriorConnect();
            }


            if (materialOverride) g.GetComponent<MeshRenderer>().material = materialOverride;
        }

        public void OnExteriorConnect() { exterior = item.holder.gameObject; }


        public void DestroyExterior()
        {
            Destroy(exterior);

            foreach (IExterior c in item.components)
                c.OnExteriorDisconnect();

            item.holder = null;
        }

        public void OnExteriorDisconnect() { exterior = null; }


        public override void OnDuplicate() { exterior = null; }

#if UNITY_EDITOR

        public override void GuiParameters()
        {
            base.GuiParameters();
            original = (GameObject)EditorGUILayout.ObjectField("Prefab: ", original, typeof(GameObject), false);
            materialOverride = (Material)EditorGUILayout.ObjectField("Override Material: ", materialOverride, typeof(Material), false);
        }


        public List<string> GetHolderComponentNames<T>() where T : Component
        {
            if (!original) return null;

            List<string> options = new List<string>();

            T t = original.GetComponent<T>() as T;
            if(t) options.Add(t.name);

            T[] d = original.GetComponentsInChildren<T>();
            if(d.Length != 0) options.AddRange(d.Where(x => !options.Contains(x.name)).Select(x => x.name));

            return options;
        }


        public void Options<T>(ref int size, List<string> names) where T : Component
        {
            List<string> options = GetHolderComponentNames<T>();

            if(options != null && options.Count != 0)
            {
                EditorGUILayout.LabelField("Max: " + options.Count);

                if(size > options.Count) size = options.Count;

                if(size < names.Count)
                {
                    names.RemoveRange(size, names.Count - size);
                    size = names.Count;
                }

                for(int i = 0; i < size; i++)
                {
                    if(i >= names.Count) names.Add("");

                    int selected = options.Contains(names[i]) ? selected = options.IndexOf(names[i]) : 0;

                    names[i] = options[EditorGUILayout.Popup(typeof(T).ToString() + i + ": ", selected, options.ToArray())];
                }
            }
            else
            {
                EditorGUILayout.LabelField("There isn't " + typeof(T).ToString() + "(s)");
            }
        }


        public static void GetComponentsNames<T>(Item item, ref int size, List<string> names) where T : Component
        {
            try
            {
                Exterior exterior = item.GetComponent<Exterior>();

                int s = EditorGUILayout.IntField("Size: ", size);

                if (s > 0) size = s;

                exterior.Options<T>(ref size, names);
            }
            catch (NullReferenceException) { EditorGUILayout.LabelField("Need Exterior type component"); }
            
        }


        public static void GetComponentName<T>(Item item, ref string name) where T : Component
        {
            try
            {
                Exterior exterior = item.GetComponent<Exterior>();

                List<string> options = exterior.GetHolderComponentNames<T>();

                int selectedIdx = options.Contains(name) ? options.IndexOf(name) : 0;

                name = options[EditorGUILayout.Popup(typeof(T).ToString() + ": ", selectedIdx, options.ToArray())];
            }
            catch (NullReferenceException) { EditorGUILayout.LabelField("Need Exterior type component"); }
        }

#endif 
    }
}
