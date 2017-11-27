using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

namespace Game.SistemaInventario
{
    public abstract class ItemComponent : ScriptableObject
    {
        public Item item;

        public virtual void AoDuplicar()
        {

        }

#if UNITY_EDITOR

        bool mostrar;

        public void OnGui()
        {
            mostrar = EditorGUILayout.Foldout(mostrar, this.ToString());

            if (mostrar)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Space(20);

                GUILayout.BeginVertical();
                GuiParametros();

                if (GUILayout.Button("Eliminar"))
                {
                    item.componentes.Remove(this);
                    DestroyImmediate(this, true);
                }

                GUILayout.EndVertical();

                GUILayout.EndHorizontal();
            }
        }

        public virtual void GuiParametros()
        {
            name = EditorGUILayout.TextField("Nome: ", name);
        }

#endif 
    }
}
