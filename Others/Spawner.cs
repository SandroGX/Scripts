#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Spawner : MonoBehaviour
{
    public int number;
    public GameObject prefab;
    public Vector2 dimensions = new Vector2(200, 200);
    public float minimunDistanceFromOthes = 5;
    public float maxAngle = 30;
    public bool followSurfaceNormal = true;
    public Vector2 rotVarY = new Vector2(0, 360);
    public float surfaceOffset;
    public LayerMask toSpawn, toNotSpawn;

    int maxTries = 70;
    List<RaycastHit> points = new List<RaycastHit>();


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(dimensions.x, 0.1f, dimensions.y));
    }

    void RandomPoints()
    {
        for (int e = 0; e < number; e++)
        {
            for (int i = 0; i < maxTries; i++)
            {
                Vector3 a = transform.rotation * new Vector3(Random.Range(-dimensions.x / 2, dimensions.x / 2), 0, Random.Range(-dimensions.y / 2, dimensions.y / 2)) + transform.position;

                RaycastHit hit;

                if (!Physics.Raycast(a, -transform.up, out hit, Mathf.Infinity, toSpawn, QueryTriggerInteraction.Ignore)) continue;

                if (Physics.Raycast(a, -transform.up, out hit, Mathf.Infinity, toNotSpawn, QueryTriggerInteraction.Ignore)) continue;

                if (90 - Vector3.Angle(transform.up, hit.normal) < maxAngle) continue;

                bool b = false;

                foreach (Vector3 f in points.Select(x => x.point))
                {
                    if (Vector3.Distance(hit.point, f) < minimunDistanceFromOthes)
                    {
                        b = true;
                        break;
                    }
                }

                if (b) continue;

                points.Add(hit);
                break;
            }
        }

    }

    public void Spawn()
    {
        points.Clear();
    
        CleanChildren();

        RandomPoints();

        foreach (RaycastHit hit in points)
        {
            GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            go.transform.parent = transform;
            go.transform.position = hit.point - transform.up * surfaceOffset;

            if (followSurfaceNormal) go.transform.up = hit.normal;
                //go.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation; ;
            

            go.transform.eulerAngles += Vector3.up * Random.Range(rotVarY.x, rotVarY.y);
        }
    }


    public void CleanChildren()
    {
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }


}
#endif
