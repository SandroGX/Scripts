using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    Text text;
    Slider barra;
    [HideInInspector]
    public Danificavel dan;
    [HideInInspector]
    public Transform danTransform;

    public bool atualizarPosicao;
    public bool destruir;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        barra = GetComponent<Slider>();

        barra.maxValue = dan.danosMax;
        barra.value = dan.danosMax - dan.danos;
        text.text = dan.danosMax - dan.danos + "/" + dan.danosMax;
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
            barra.maxValue = dan.danosMax;
            barra.value = dan.danosMax - dan.danos;
            text.text = dan.danosMax - dan.danos + "/" + dan.danosMax;

            if (atualizarPosicao)
                transform.position = Camera.main.WorldToScreenPoint(danTransform.position);
        }

    }

	
}
