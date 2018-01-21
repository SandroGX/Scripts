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

    public Character character;

    void Update()
    {
        if (!character) return;

        Barra(barraDeDanos, textoDanos, "Life", character.danificavel.danificavel.life.value, character.danificavel.danificavel.life.maxValue, false);
        Barra(barraDeStamina, textoStamina, "Stamina", character.stamina.value, character.stamina.maxValue, false);
    }


    void Barra(Image barraFill, Text texto, string nome, float valor, float valorMax, bool inverter)
    {
        float v = valor / valorMax;
        barraFill.fillAmount = inverter ? 1 - v : v;

        texto.text = nome + ": " + (inverter ? valorMax - valor : valor) + "/" + valorMax;
    }
}
