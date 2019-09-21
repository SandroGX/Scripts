using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GX.TerrainLODSystem
{
    public class TerrainLODManager : MonoBehaviour
    {

        public TerrainLOD[] terrains;
        Transform playerCamera;

        public int minTerrainSize;
        public List<Vector3Int> neighbors = new List<Vector3Int>();
        public List<Vector3Int> distantNeighbors = new List<Vector3Int>();
        bool blender;
        int[] wall;

        Vector3Int last;


        void Start()
        {
            playerCamera = Camera.main.transform;

            terrains = GetComponentsInChildren<TerrainLOD>();

            wall = new int[6];

            last = new Vector3Int(Mathf.FloorToInt(playerCamera.position.x / minTerrainSize), 
                                Mathf.FloorToInt(playerCamera.position.y / minTerrainSize),
                                Mathf.FloorToInt(playerCamera.position.z / minTerrainSize));

            UpdateTerrain(last);
        }


        void Update()
        {
            Vector3Int current = new Vector3Int(Mathf.FloorToInt(playerCamera.position.x / minTerrainSize),
                                                Mathf.FloorToInt(playerCamera.position.y / minTerrainSize),
                                                Mathf.FloorToInt(playerCamera.position.z / minTerrainSize));

            if (last != current)
            {
                UpdateTerrain(current);
                last = current;
            }
        }


        void UpdateTerrain(Vector3Int v)
        {
            SetNeighbors(v);

            for(int a = 0; a < terrains.Length; a++)
            {
                if (neighbors.Contains(terrains[a].ID))
                {
                    terrains[a].LOD(2);
                    terrains[a].DeactivateWalls();
                }
                else if (distantNeighbors.Contains(terrains[a].ID))
                {
                    terrains[a].LOD(1);
                    terrains[a].ActivateWall(wall);
                }
                else
                {
                    terrains[a].LOD(0);
                    terrains[a].DeactivateWalls();
                }
            }

            Debug.Log("Hello, updated :)");
        }


        void SetNeighbors(Vector3Int v)
        {
            neighbors.Clear();
            distantNeighbors.Clear();

            wall[0] = v.z + 2;
            wall[1] = v.z - 1;
            wall[2] = v.x + 2;
            wall[3] = v.x - 1;
            wall[4] = v.y + 2;
            wall[5] = v.y - 1;

            for (int a = -1; a < 3; a++)
            {
                for (int b = -1; b < 3; b++)
                {
                    for (int c = -1; c < 3; c++)
                    {
                        if(a == -1 || b == -1 || c == -1 || a == 2 || b == 2 || c == 2)
                            distantNeighbors.Add(new Vector3Int(v.x + a, v.y + b, v.z + c));
                        else
                             neighbors.Add(new Vector3Int(v.x + a, v.y + b, v.z + c));
                    }
                }
            }
        }
    }
}
