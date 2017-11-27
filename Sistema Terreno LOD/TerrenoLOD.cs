using UnityEngine;
using System.Collections;

namespace Game.SistemaTerrenoLOD
{
    [ExecuteInEditMode]
    public class TerrenoLOD : MonoBehaviour
    {

        public Vector3 ID;
        public bool blender;
        GameObject hi;
        GameObject lo;
        GameObject paredeF;
        GameObject paredeB;
        GameObject paredeR;
        GameObject paredeL;
        GameObject paredeU;
        GameObject paredeD;



        void Awake()
        {

            if (transform.childCount == 2 || transform.childCount == 8)
            {
                hi = transform.GetChild(0).gameObject;
                lo = transform.GetChild(1).gameObject;

                if (transform.childCount == 2)
                {
                    paredeF = null;
                    paredeB = null;
                    paredeR = null;
                    paredeL = null;
                    paredeU = null;
                    paredeD = null;
                }
                else
                {
                    paredeF = transform.GetChild(2).gameObject;
                    paredeB = transform.GetChild(3).gameObject;
                    paredeR = transform.GetChild(4).gameObject;
                    paredeL = transform.GetChild(5).gameObject;
                    paredeU = transform.GetChild(6).gameObject;
                    paredeD = transform.GetChild(7).gameObject;
                }
            }
            else Debug.LogError("A");

            if (hi.GetComponent<MeshCollider>() == null)
                hi.AddComponent<MeshCollider>();

            if(blender)
            ID = new Vector3(transform.localPosition.x, transform.localPosition.z, -transform.localPosition.y) / GetComponentInParent<TerrenoLODManager>().minTerrTam;
            else ID = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) / GetComponentInParent<TerrenoLODManager>().minTerrTam;

            LOD(2);
            DesativarParedes();

            gameObject.name = transform.root.gameObject.name + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (hi != null)
                hi.name = "HI" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (lo != null)
                lo.name = "LO" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (paredeF != null)
                paredeF.name = "Parede F" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (paredeB != null)
                paredeB.name = "Parede B" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (paredeR != null)
                paredeR.name = "Parede R" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (paredeL != null)
                paredeL.name = "Parede L" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (paredeU != null)
                paredeU.name = "Parede U" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

            if (paredeD != null)
                paredeD.name = "Parede D" + " (" + ID.x + ", " + ID.y + ", " + ID.z + ")";

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

        public void AtivarParedes(int[] parede)
        {
            if (transform.childCount == 8)
            {
                if (ID.z == parede[0])
                    paredeF.SetActive(true);
                else paredeF.SetActive(false);

                if (ID.z == parede[1])
                    paredeB.SetActive(true);
                else paredeB.SetActive(false);

                if (ID.x == parede[2])
                    paredeR.SetActive(true);
                else paredeR.SetActive(false);

                if (ID.x == parede[3])
                    paredeL.SetActive(true);
                else paredeL.SetActive(false);

                if (ID.y == parede[4])
                    paredeU.SetActive(true);
                else paredeU.SetActive(false);

                if (ID.y == parede[5])
                    paredeD.SetActive(true);
                else paredeD.SetActive(false);
            }

        }

        public void DesativarParedes()
        {
            if (transform.childCount == 8)
            {
                paredeF.SetActive(false);
                paredeB.SetActive(false);
                paredeR.SetActive(false);
                paredeL.SetActive(false);
                paredeU.SetActive(false);
                paredeD.SetActive(false);
            }
        }

    }
}
