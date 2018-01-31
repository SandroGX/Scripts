using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    Text text;
    Slider barra;
    [HideInInspector]
    public Damageable dan;
    [HideInInspector]
    public Transform danTransform;

    public bool atualizarPosicao;
    public bool destruir;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        barra = GetComponent<Slider>();

        Show();
    }


    void Update()
    {
        if (dan == null)
        {
            if (destruir)
                Destroy(gameObject);
        }
        else
        {
            Show();

            if (atualizarPosicao)
                transform.position = Camera.main.WorldToScreenPoint(danTransform.position);
        }
    }


    void Show()
    {
        barra.maxValue = dan.life.maxValue;
        barra.value = dan.life.value;
        text.text = dan.life.value + "/" + dan.life.maxValue;
    }

	
}
