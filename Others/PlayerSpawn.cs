using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.InventorySystem;

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
        if (Game.GameManager.PLAYER == null) Game.GameManager.PLAYER = playerPrefab.GetComponent<Exterior>().Create(transform.position, transform.rotation);
    }
}
