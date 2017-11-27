using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.SistemaInventario
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 0)]
    [System.Serializable]
    public class Item : ScriptableObject
    {
        //static List<Item> items = new List<Item>();

        public List<ItemComponent> componentes = new List<ItemComponent>();

        public Item original;
        public ItemHolder holder;



        public static Item Duplicar(Item aDuplicar)
        {
            Item duplicado = Instantiate(aDuplicar);

            duplicado.componentes.Clear();

            if (aDuplicar.original)
                duplicado.original = aDuplicar.original;
            else duplicado.original = aDuplicar;

            duplicado.name = duplicado.original.name;

            foreach (ItemComponent c in aDuplicar.componentes)
            {
                ItemComponent cd = Instantiate(c);
                duplicado.componentes.Add(cd);

                cd.name = c.name;
                cd.item = duplicado;
            }

            foreach (ItemComponent cd in duplicado.componentes)
                cd.AoDuplicar();


            return duplicado;
        }



        public T GetComponent<T>() where T : ItemComponent
        {
            return componentes.Where(x => x as T != null).First() as T;
        }

        public T[] GetComponents<T>() where T : ItemComponent
        {
            return componentes.Where(x => x as T != null).Select(x => x as T).ToArray();
        }



#if UNITY_EDITOR

        static public List<string> todosComps = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().
            Where(x => x.IsSubclassOf(typeof(ItemComponent))).Select(x => x.ToString()).ToList();

        Vector2 scroll = new Vector2();

        public void OnGui()
        {
            GUILayout.BeginScrollView(scroll, false, true);

            name = EditorGUILayout.TextField("Nome: ", name);

            for (int i = 0; i < componentes.Count; i++)
                componentes[i].OnGui();

            GUILayout.EndScrollView();

        }

#endif 
    }
}
