using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace GX.InventorySystem
{
    public abstract class ItemComponent : ScriptableObject
    {
        public Item item;

        public virtual void OnDuplicate()
        {

        }

#if UNITY_EDITOR

        bool show;

        public void OnGui()
        {
            show = EditorGUILayout.Foldout(show, this.ToString());

            if (show)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Space(20);

                GUILayout.BeginVertical();

                GuiParameters();
                if (GUILayout.Button("Eliminate"))
                {
                    item.components.Remove(this);
                    DestroyImmediate(this, true);
                }

                GUILayout.EndVertical();

                GUILayout.EndHorizontal();
            }
        }

        public virtual void GuiParameters()
        {
            name = EditorGUILayout.TextField("Name: ", name);
        }

#endif 
    }
}
