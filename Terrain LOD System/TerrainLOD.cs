using UnityEngine;
using System.Collections;

namespace Game.TerrainLODSystem
{
    [ExecuteInEditMode]
    public class TerrainLOD : MonoBehaviour
    {

        public Vector3Int ID;
        public bool blender;
        GameObject hi;
        GameObject lo;
        GameObject wallF;
        GameObject wallB;
        GameObject wallR;
        GameObject wallL;
        GameObject wallU;
        GameObject wallD;


        void Awake()
        {

            if (transform.childCount == 2 || transform.childCount == 8)
            {
                hi = transform.GetChild(0).gameObject;
                lo = transform.GetChild(1).gameObject;

                if (transform.childCount == 2)
                {
                    wallF = null;
                    wallB = null;
                    wallR = null;
                    wallL = null;
                    wallU = null;
                    wallD = null;
                }
                else
                {
                    wallF = transform.GetChild(2).gameObject;
                    wallB = transform.GetChild(3).gameObject;
                    wallR = transform.GetChild(4).gameObject;
                    wallL = transform.GetChild(5).gameObject;
                    wallU = transform.GetChild(6).gameObject;
                    wallD = transform.GetChild(7).gameObject;
                }
            }
            else Debug.LogError("You need 2 mesh for the terrain LOD or 8 for the terrain and walls");

            if (hi.GetComponent<MeshCollider>() == null)
                hi.AddComponent<MeshCollider>();

            int m = GetComponentInParent<TerrainLODManager>().minTerrainSize;

            if (blender)
                ID = new Vector3Int((int)transform.localPosition.x/m, (int)transform.localPosition.z, -((int)transform.localPosition.y/m));
            else ID = new Vector3Int((int)transform.localPosition.x/m, (int)transform.localPosition.y/m, (int)transform.localPosition.z/m);

            LOD(2);
            DeactivateWalls();

            gameObject.name = transform.root.gameObject.name + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (hi != null) hi.name = "HI " + ID;

            if (lo != null) lo.name = "LO " + ID;

            if (wallF != null) wallF.name = "Wall F " + ID;

            if (wallB != null) wallB.name = "Wall B " + ID;

            if (wallR != null) wallR.name = "Wall R " + ID;

            if (wallL != null) wallL.name = "Wall L " + ID;

            if (wallU != null) wallU.name = "Wall U " + ID;

            if (wallD != null) wallD.name = "Wall D " + ID;

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

        public void ActivateWall(int[] parede)
        {
            if (transform.childCount == 8)
            {
                if (ID.z == parede[0]) wallF.SetActive(true);
                else wallF.SetActive(false);

                if (ID.z == parede[1]) wallB.SetActive(true);
                else wallB.SetActive(false);

                if (ID.x == parede[2]) wallR.SetActive(true);
                else wallR.SetActive(false);

                if (ID.x == parede[3]) wallL.SetActive(true);
                else wallL.SetActive(false);

                if (ID.y == parede[4]) wallU.SetActive(true);
                else wallU.SetActive(false);

                if (ID.y == parede[5]) wallD.SetActive(true);
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
