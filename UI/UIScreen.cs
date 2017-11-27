using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [HideInInspector]
    public Transform i;

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(i.position);
    }
}
