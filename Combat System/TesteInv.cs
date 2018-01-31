using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.InventorySystem
{
    public class TesteInv : ItemComponent, IExterior
    {
        [SerializeField]
        string hitboxName;
        Hitbox hitbox;

        public void OnCreate()
        {
            hitbox = item.holder.GetHolderComponent<Hitbox>(hitboxName);
            if(hitbox) hitbox.OnHitEnter += Teste;
        }

        void Teste(HitInfo h)
        {
            
        }


#if UNITY_EDITOR

        Exterior exterior;

        public override void GuiParameters()
        {
            base.GuiParameters();

            
            if (exterior)
            {
                List<string> opcoes = exterior.GetHolderComponentNames<Hitbox>();

                if (opcoes.Count != 0)
                {
                    int a;

                    if (opcoes.Contains(hitboxName))
                        a = opcoes.IndexOf(hitboxName);
                    else a = 0;

                    hitboxName = opcoes[EditorGUILayout.Popup("HitBox: ", a, opcoes.ToArray())];
                }
                else EditorGUILayout.LabelField("There aren't hitboxes");

            }
            else
            {
                exterior = item.GetComponent<Exterior>();
                EditorGUILayout.LabelField("You need a component of type Exterior");
            }

        }
#endif
    }
}
