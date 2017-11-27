using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAA : MonoBehaviour
{
    List<GameObject> inimigos = new List<GameObject>();
    public GameObject prefab;
    public List<Transform> spawnPoints = new List<Transform>();
    Transform player;


    void Awake()
    {
        for(int i = 0; i < spawnPoints.Count; i++)
        {
            inimigos.Add(Instantiate(prefab, spawnPoints[i].position, spawnPoints[i].rotation));
        }
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        int n = 0;

        foreach(GameObject i in inimigos)
        {
            if (i != null)
                n++;
        }

        if (n < spawnPoints.Count)
        {
            float distancia = float.MinValue;
            Transform spawnPoint = spawnPoints[0];

            for(int i = 0; i < spawnPoints.Count; i++)
            {
                float distAtual = Vector3.Distance(spawnPoints[i].position, player.position);
                if (distAtual > distancia)
                {
                    distancia = distAtual;
                    spawnPoint = spawnPoints[i];
                }
            }

            for (int i = 0; i < spawnPoints.Count - n; i++)
            {
                inimigos.Add(Instantiate(prefab, spawnPoint.position, spawnPoint.rotation));
            }
        }
    }
    
}
