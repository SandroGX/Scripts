using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace GX.InventorySystem
{
    public class ItemEditor : EditorWindow
    {
        string newCompType = Item.allComponents[0];

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
    
                newCompType = Item.allComponents[EditorGUILayout.Popup(Item.allComponents.IndexOf(newCompType), Item.allComponents.ToArray())];
                if(GUILayout.Button("Create component"))
                {
                    ItemComponent c = CreateInstance(newCompType) as ItemComponent;
                    if (c)
                    {
                        c.item = item;
                        c.name = "New " + c.GetType().ToString();
                        SODatabase.Add(item, c, item.components);
                    }
                }

                GUILayout.EndHorizontal();

                if (GUILayout.Button("Save"))
                {
                    EditorUtility.SetDirty(item);
                    foreach (ItemComponent c in item.components) EditorUtility.SetDirty(c);
                }
            }
            else
            {

                EditorGUILayout.LabelField("Choose an Item");

                item = Selection.activeObject as Item;
            }

            GUILayout.EndVertical();
        }
    }
}
