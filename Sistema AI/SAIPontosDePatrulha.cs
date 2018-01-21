using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAIPontosDePatrulha : MonoBehaviour
{
    public Transform patrulha;
    public List<Transform> pontosDePatrulha = new List<Transform>();

    void Awake()
    {
        if(patrulha != null)
            pontosDePatrulha.AddRange(patrulha.GetComponentsInChildren<Transform>());
    } 
}
