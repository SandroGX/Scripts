using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    Text text;
    Slider bar;
    [HideInInspector]
    public Damageable dan;
    [HideInInspector]
    public Transform danTransform;

    public bool updatePosition;
    public bool destroy;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        bar = GetComponent<Slider>();

        Show();
    }


    void Update()
    {
        if (dan == null)
        {
            if (destroy) Destroy(gameObject);
        }
        else
        {
            Show();
            if (updatePosition) transform.position = Camera.main.WorldToScreenPoint(danTransform.position);
        }
    }


    void Show()
    {
        bar.maxValue = dan.life.maxValue;
        bar.value = dan.life.value;
        text.text = dan.life.value + "/" + dan.life.maxValue;
    }

	
}
