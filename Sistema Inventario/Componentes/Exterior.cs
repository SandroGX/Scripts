using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.SistemaInventario
{
    [System.Serializable]
    public class Exterior : ItemComponent, IExterior
    {
        public GameObject original, exterior;


        public override void AoDuplicar()
        {
            exterior = null;
        }



        public GameObject Criar(Vector3 posicao)
        {
            GameObject e = Instantiate(original, posicao, Quaternion.identity);
            C(e);
            return e;
        }

        public GameObject Criar(Vector3 posicao, Quaternion rotacao)
        {
            GameObject e = Instantiate(original, posicao, rotacao);
            C(e);
            return e;
        }

        public GameObject Criar(Transform parent)
        {
            GameObject e = Instantiate(original, parent);
            C(e);
            return e;
        }

        public GameObject Criar(Transform parent, bool instantiateInWorldSpace = false)
        {
            GameObject e = Instantiate(original, parent, instantiateInWorldSpace);
            C(e);
            return e;
        }

        public GameObject Criar(Vector3 posicao, Quaternion rotacao, Transform parent)
        {
            GameObject e = Instantiate(original, posicao, rotacao, parent);
            C(e);
            return e;
        }



        void C(GameObject g)
        {

            ItemHolder h = g.AddComponent<ItemHolder>();

            Item i;

            if (!item.original)
                i = Item.Duplicar(item);
            else i = item;

            i.holder = h;
            h.item = i;

            foreach (ItemComponent c in i.componentes)
            {
                IExterior e = c as IExterior;

                if(e != null)
                    e.OnCriado();
            }

            
        }



        public void OnCriado()
        {
            exterior = item.holder.gameObject;
        }



#if UNITY_EDITOR

        public override void GuiParametros()
        {
            base.GuiParametros();
            original = (GameObject)EditorGUILayout.ObjectField("Prefab: ", original, typeof(GameObject), false);
        }



        public List<string> GetHolderComponentNames<T>() where T : Component
        {
            List<string> opcoes = new List<string>();

            T t = original.GetComponent<T>() as T;

            if(t)
                opcoes.Add(t.name);

            T[] d = original.GetComponentsInChildren<T>();

            if(d.Length != 0)
                opcoes.AddRange(d.Where(x => !opcoes.Contains(x.name)).Select(x => x.name));

            return opcoes;
        }



        public void Opcoes<T>(ref int size, List<string> names) where T : Component
        {
            List<string> opcoes = GetHolderComponentNames<T>();

            if (opcoes.Count != 0)
            {

                EditorGUILayout.LabelField("Max: " + opcoes.Count);

                if (size >= opcoes.Count) size = opcoes.Count;

                if (size < names.Count)
                {
                    names.RemoveRange(size, names.Count - size);
                    size = names.Count;
                }

                for (int i = 0; i < size; i++)
                {
                    int a;

                    if (i >= names.Count)
                        names.Add("");

                    if (opcoes.Contains(names[i]))
                        a = opcoes.IndexOf(names[i]);
                    else a = 0;

                    names[i] = opcoes[EditorGUILayout.Popup(typeof(T).ToString() + i + ": ", a, opcoes.ToArray())];
                }
            }
            else
            {
                EditorGUILayout.LabelField("Nao ha " + typeof(T).ToString() + "(s)");
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
