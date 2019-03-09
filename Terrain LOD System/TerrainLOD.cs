using UnityEngine;
using System.Collections;

namespace Game.TerrainLODSystem
{
    [ExecuteInEditMode]
    public class TerrainLOD : MonoBehaviour
    {

        public Vector3Int ID;
        GameObject hi;
        GameObject lo;
        GameObject wallF;
        GameObject wallB;
        GameObject wallR;
        GameObject wallL;
        GameObject wallU;
        GameObject wallD;


        private void Awake()
        {
            if (transform.childCount == 2 || transform.childCount == 8)
            {
                hi = transform.GetChild(0).gameObject;
                lo = transform.GetChild(1).gameObject;
                hi.name = "HI";
                lo.name = "LO";

                if (transform.childCount == 8)
                {
                    wallF = transform.GetChild(2).gameObject;
                    wallB = transform.GetChild(3).gameObject;
                    wallR = transform.GetChild(4).gameObject;
                    wallL = transform.GetChild(5).gameObject;
                    wallU = transform.GetChild(6).gameObject;
                    wallD = transform.GetChild(7).gameObject;

                    wallF.name = "Wall F";
                    wallB.name = "Wall B";
                    wallR.name = "Wall R";
                    wallL.name = "Wall L";
                    wallU.name = "Wall U";
                    wallD.name = "Wall D";
                }
                else wallF = wallB = wallR = wallL = wallU = wallD = null;
            }
            else { Debug.LogError("You need 2 mesh for the terrain LOD or 8 for the terrain and walls"); return; }


            if (hi.GetComponent<MeshCollider>() == null)
                hi.AddComponent<MeshCollider>();

            int m = GetComponentInParent<TerrainLODManager>().minTerrainSize;

            ID = new Vector3Int((int)transform.localPosition.x/m, (int)transform.localPosition.y/m, (int)transform.localPosition.z/m);
            gameObject.name = transform.root.gameObject.name + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            LOD(2);
            DeactivateWalls();

        }


        public void LOD(int lodLevel)
        {
            switch (lodLevel)
            {
                case 0:
                    hi.SetActive(false);
                    lo.SetActive(false);
                    break;
                case 1:
                    hi.SetActive(false);
                    lo.SetActive(true);
                    break;
                case 2:
                    hi.SetActive(true);
                    lo.SetActive(false);
                    break;

            }
        }

        public void ActivateWall(int[] wall)
        {
            if (transform.childCount == 8)
            {
                if (ID.z == wall[0]) wallF.SetActive(true);
                else wallF.SetActive(false);

                if (ID.z == wall[1]) wallB.SetActive(true);
                else wallB.SetActive(false);

                if (ID.x == wall[2]) wallR.SetActive(true);
                else wallR.SetActive(false);

                if (ID.x == wall[3]) wallL.SetActive(true);
                else wallL.SetActive(false);

                if (ID.y == wall[4]) wallU.SetActive(true);
                else wallU.SetActive(false);

                if (ID.y == wall[5]) wallD.SetActive(true);
                else wallD.SetActive(false);
            }

        }

        public void DeactivateWalls()
        {
            if (transform.childCount == 8)
            {
                wallF.SetActive(false);
                wallB.SetActive(false);
                wallR.SetActive(false);
                wallL.SetActive(false);
                wallU.SetActive(false);
                wallD.SetActive(false);
            }
        }

    }
}
