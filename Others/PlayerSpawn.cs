using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GX.InventorySystem;

public class PlayerSpawn : MonoBehaviour {

    public Item playerPrefab;


	void Start ()
    {
        Spawn();
    }
	
	
	void Update ()
    {
        Spawn();
    }

    void Spawn()
    {
        if (GX.GameManager.PLAYER == null) GX.GameManager.PLAYER = playerPrefab.GetComponent<Exterior>().Create(transform.position, transform.rotation);
    }
}
