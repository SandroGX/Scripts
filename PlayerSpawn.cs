using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    GameObject player;
    public GameObject playerPrefab;


	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	
	void Update ()
    {
        if (player == null)
            player = Instantiate(playerPrefab, transform.position, transform.rotation);
	}
}
