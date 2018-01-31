using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.InventorySystem;

public class MultiSpawner : MonoBehaviour
{
    public Item[] items;
    public GameObject[] gameObjects;
    public int maxSpawn, maxSpawnPerInterval, initialSpawn, toSpawnPerInterval;
    int totalSpawn;
    public float spawnTimeInterval;
    List<int> usedPositions = new List<int>();
    //List<Item> spawnedItems = new List<Item>();
    List<GameObject> spawnedGameObjects = new List<GameObject>();
    
    void Start()
    {
        Spawning(initialSpawn);
        StartCoroutine(S());
    }

    IEnumerator S()
    {
        yield return new WaitForSeconds(spawnTimeInterval);

        while (true)
        {
            if(ShouldSpawn()) Spawning(toSpawnPerInterval);
            yield return new WaitForSeconds(spawnTimeInterval); 
        }
    }

    
    void Spawning(int toSpawn)
    {
        for (int i = 0; i < spawnedGameObjects.Count; ++i) { if (!spawnedGameObjects[i]) spawnedGameObjects.RemoveAt(i); }

        usedPositions.Clear();

        toSpawn = Mathf.Clamp(toSpawn, 0, transform.childCount - 1);
        for (int i = 0; i < toSpawn && ShouldSpawn(); ++i) RandomSpawn();
    }


    bool ShouldSpawn()
    {
        return (totalSpawn < maxSpawn || maxSpawn == 0) && spawnedGameObjects.Count < maxSpawnPerInterval;
    }


    int RandomLocation()
    {
        int r;
        do r = Random.Range(0, transform.childCount - 1);
        while (usedPositions.Contains(r));
        usedPositions.Add(r);
        return r;
    }


    void RandomSpawn()
    {
        ++totalSpawn;
        Transform tf = transform.GetChild(RandomLocation());
        int r = Random.Range(0, items.Length + gameObjects.Length - 1);

        if (r < items.Length) Spawn(items[r], tf);
        else Spawn(gameObjects[r - items.Length], tf);
    }


    void Spawn(Item item, Transform tf)
    {
        spawnedGameObjects.Add(item.GetComponent<Exterior>().Create(tf.position, tf.rotation));
    }

    void Spawn(GameObject gameObject, Transform tf)
    {
        spawnedGameObjects.Add(Instantiate(gameObject, tf.position, tf.rotation));
    }
}
