using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using Game.SistemaMotor;

namespace Game.SistemaInventario
{
    [System.Serializable]
    public class Slot : ItemComponent
    {
        public Item guardado;
        public Item Default;


        public override void AoDuplicar()
        {

            if(Default)
                Default = Item.Duplicar(Default);

            if(guardado)
                guardado = Item.Duplicar(guardado);

            if (!guardado && Default)
                guardado = Default;
        }



        public void PorItem(Item aPor, out Item aRetirar)
        {
            if (Condicoes(aPor))
            {
                if (guardado == Default)
                    aRetirar = null;
                else aRetirar = guardado;

                if (aPor)
                    guardado = aPor;
                else guardado = Default;

                AoPor(guardado);
                AoRetirar(aRetirar);
            }
            else aRetirar = null;
        }



        bool Condicoes(Item aAvaliar)
        {
            return true;
        }



        protected virtual void AoPor(Item aPor)
        {
             
        }


        protected virtual void AoRetirar(Item aRetirar)
        {

        }



#if UNITY_EDITOR

        public override void GuiParametros()
        {
            base.GuiParametros();

            Default = EditorGUILayout.ObjectField("Default: ", Default, typeof(Item), false) as Item;
            guardado = EditorGUILayout.ObjectField("Item: ", guardado, typeof(Item), false) as Item;
        }

#endif
    }
}
