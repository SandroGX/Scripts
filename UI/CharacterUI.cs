using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Image barraDeDanos;
    public Text textoDanos;
    public Image barraDeStamina;
    public Text textoStamina;

    [HideInInspector]
    public Character character;

    void Update()
    {
        a(barraDeDanos, textoDanos, "Danos",character.danificavel.danos, character.danificavel.danosMax, true);
        a(barraDeStamina, textoStamina, "Stamina",character.stamina, character.staminaMax, false);
    }


    void a(Image barraFill, Text texto, string nome, float valor, float valorMax, bool inverter)
    {
        barraFill.fillAmount = (inverter) ? 1 - valor / valorMax : valor / valorMax;

        texto.text = nome + ": " + (inverter ? valorMax - valor : valor) + "/" + valorMax;
    }
}
