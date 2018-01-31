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


        public override void OnDuplicate()
        {
            exterior = null;
        }


        public GameObject Create(Vector3 position)
        {
            GameObject g = Instantiate(original, position, Quaternion.identity);
            CreateItem(g);
            return g;
        }

        public GameObject Create(Vector3 position, Quaternion rotation)
        {
            GameObject g = Instantiate(original, position, rotation);
            CreateItem(g);
            return g;
        }

        public GameObject Create(Transform parent)
        {
            GameObject g = Instantiate(original, parent);
            CreateItem(g);
            return g;
        }

        public GameObject Create(Transform parent, bool instantiateInWorldSpace = false)
        {
            GameObject g = Instantiate(original, parent, instantiateInWorldSpace);
            CreateItem(g);
            return g;
        }

        public GameObject Create(Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject g = Instantiate(original, position, rotation, parent);
            CreateItem(g);
            return g;
        }


        void CreateItem(GameObject g)
        {

            ItemHolder h = g.AddComponent<ItemHolder>();

            Item i;
            if (!item.original)i = Item.Duplicate(item);
            else i = item;

            i.holder = h;
            h.item = i;

            foreach (ItemComponent c in i.components)
            {
                IExterior e = c as IExterior;
                if(e != null) e.OnCreate();
            }

            if (materialOverride) g.GetComponent<MeshRenderer>().material = materialOverride;
        }

        public void OnCreate()
        {
            exterior = item.holder.gameObject;
        }


        public void Destroy()
        {
            Destroy(exterior);
        }


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


        public void Opcoes<T>(ref int size, List<string> names) where T : Component
        {
            List<string> options = GetHolderComponentNames<T>();

            if (options != null && options.Count != 0)
            {
                EditorGUILayout.LabelField("Max: " + options.Count);

                if (size >= options.Count) size = options.Count;

                if (size < names.Count)
                {
                    names.RemoveRange(size, names.Count - size);
                    size = names.Count;
                }

                for (int i = 0; i < size; i++)
                {
                    int a;

                    if (i >= names.Count) names.Add("");

                    if (options.Contains(names[i])) a = options.IndexOf(names[i]);
                    else a = 0;

                    names[i] = options[EditorGUILayout.Popup(typeof(T).ToString() + i + ": ", a, options.ToArray())];
                }
            }
            else
            {
                EditorGUILayout.LabelField("There isn't " + typeof(T).ToString() + "(s)");
            }
        }


        public static void GetComponentsNames<T>(Exterior exterior, ref int size, List<string> names) where T : Component
        {

            if (exterior)
            {
                int s = EditorGUILayout.IntField("Size: ", size);

                if (s > 0) size = s;

                exterior.Opcoes<T>(ref size, names);

                //size = names.Length;
            }
            else
                EditorGUILayout.LabelField("Precisa de um componente do tipo Exterior");
            
        }


        public static void GetComponentsName<T>(Exterior exterior, ref string name) where T : Component
        {

            if (exterior)
            {
                List<string> opcoes = exterior.GetHolderComponentNames<T>();

                int a;

                if (opcoes.Contains(name))
                    a = opcoes.IndexOf(name);
                else a = 0;

                name = opcoes[EditorGUILayout.Popup(typeof(T).ToString() + ": ", a, opcoes.ToArray())];
            }
            else
            {
                EditorGUILayout.LabelField("Precisa de um componente do tipo Exterior");
            }
        }

#endif 
    }
}
