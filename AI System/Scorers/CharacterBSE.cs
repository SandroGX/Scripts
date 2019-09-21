using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GX.AISystem
{
    public class CharacterBSE : BiggerSmallerEqual, IAISObjectScorer<Character>, IAISVarScorer
    {
        [SerializeField] Character toScore;
        public Character ToScore
        {
            get { return toScore; }
            set { toScore = value; }
        }

        [SerializeField] AISVarSingle toScoreVariable;
        public AISVarSingle ToScoreVariable
        {
            get { return toScoreVariable; }
            set { toScoreVariable = value; }
        }

        enum ToCompare { damage, damage_Percentage, stamina, stamina_Percentage }
        [SerializeField] ToCompare toComp;

        public override int CalculateScore(AISController controller)
        {
            return Score(controller, ToScore);
        }

        public int Score(AISController controller, Character character)
        {
            if (!character) return 0;

            switch (toComp)
            {
                case ToCompare.damage:              toCompare = character.damageable.Life.value; break;
                case ToCompare.damage_Percentage:   toCompare = (character.damageable.Life.value / character.damageable.Life.maxValue) * 100; break;
                case ToCompare.stamina:             toCompare = character.stamina.value; break;
                case ToCompare.stamina_Percentage:  toCompare = (character.stamina.value / character.stamina.maxValue) * 100; break;
            }

            return base.CalculateScore(controller);
        }

        public int Score(AISController controller, AISVarSingle var)
        {
            return Score(controller, var.@object as Character);
        }

        public int Score(AISController controller, AISVarList var, int idx)
        {
            return Score(controller, var.@object[idx] as Character);
        }



#if UNITY_EDITOR
        protected override void GuiParameters()
        {
            toComp = (ToCompare)EditorGUILayout.EnumPopup("To Compare: ", toComp);
            toCompName = toComp.ToString();
            ToScore = EditorGUILayout.ObjectField("To Score: ", ToScore, typeof(Character), false) as Character;

            base.GuiParameters();
        }
#endif
    }
}
