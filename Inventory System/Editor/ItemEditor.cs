using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Game.InventorySystem
{
    public class ItemEditor : EditorWindow
    {
        string novoCompTipo = Item.allComponents[0];

        Item item;

        [MenuItem("Window/Item Editor")]
        static void Initialize()
        {
            ItemEditor window = GetWindow<ItemEditor>();
            window.minSize = new Vector2(1000, 500);
            window.Show();
        }


        void OnGUI()
        {
            GUILayout.BeginVertical();
            item = EditorGUILayout.ObjectField("Item: ", item, typeof(Item), false) as Item;

            if (item)
            {
                item.OnGui();

                GUILayout.BeginHorizontal();

                novoCompTipo = Item.allComponents[EditorGUILayout.Popup(Item.allComponents.IndexOf(novoCompTipo), Item.allComponents.ToArray())];

                if(GUILayout.Button("Criar componente"))
                {
                    ItemComponent c = CreateInstance(novoCompTipo) as ItemComponent;

                    if (c)
                    {
                        item.components.Add(c);
                        c.item = item;
                        c.name = "Novo " + c.GetType().ToString(); 
                        AssetDatabase.AddObjectToAsset(c, item);
                    }
                }

                GUILayout.EndHorizontal();

                if (GUILayout.Button("Guardar"))
                {
                    EditorUtility.SetDirty(item);

                    foreach (ItemComponent c in item.components)
                        EditorUtility.SetDirty(c);
                }
            }
            else
            {

                EditorGUILayout.LabelField("Escolhe um item");

                item = Selection.activeObject as Item;
            }

            GUILayout.EndVertical();
        }
    }
}
