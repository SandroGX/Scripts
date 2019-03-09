using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Image lifeBar;
    public Text lifeText;
    public Image staminaBar;
    public Text staminaText;

    public Color staminaColor, staminaDepletedColor;

    public Character character;

    void Update()
    {
        if (!character) return;

        Bar(lifeBar, lifeText, "Life", character.damageable.Life.value, character.damageable.Life.maxValue, false);
        Bar(staminaBar, staminaText, "Stamina", character.stamina.value, character.stamina.maxValue, false);

        SetStaminaColor();
    }


    void Bar(Image barFill, Text text, string name, float value, float maxValue, bool invert)
    {
        float v = value / maxValue;
        barFill.fillAmount = invert ? 1 - v : v;

        text.text = name + ": " + (int)(invert ? maxValue - value : value) + "/" + maxValue;
    }


    void SetStaminaColor()
    {
        staminaBar.color = character.tired ? staminaDepletedColor : staminaColor;
    }
}
