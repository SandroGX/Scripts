using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.SistemaTerrenoLOD
{
    public class TerrenoLODManager : MonoBehaviour
    {

        public TerrenoLOD[] terrenos;
        Transform camara;

        public int minTerrTam;
        public List<Vector3> vizinhos = new List<Vector3>();
        public List<Vector3> vizinhosDistantes = new List<Vector3>();
        bool blender;
        int[] paredes;

        int ultimoX;
        int ultimoY;
        int ultimoZ;



        void Start()
        {
            camara = GameObject.FindGameObjectWithTag("Player").transform;

            terrenos = GetComponentsInChildren<TerrenoLOD>();

            paredes = new int[6];

            ultimoX = Mathf.FloorToInt(camara.position.x / minTerrTam);
            ultimoY = Mathf.FloorToInt(camara.position.y / minTerrTam);
            ultimoZ = Mathf.FloorToInt(camara.position.z / minTerrTam);

            RecarregerTerrenos(ultimoX, ultimoY, ultimoZ);
        }



        void Update()
        {
            int atualX = Mathf.FloorToInt(camara.position.x / minTerrTam);
            int atualY = Mathf.FloorToInt(camara.position.y / minTerrTam);
            int atualZ = Mathf.FloorToInt(camara.position.z / minTerrTam);

            if (ultimoX != atualX || ultimoY != atualY || ultimoZ != atualZ)
            {
                RecarregerTerrenos(atualX, atualY, atualZ);
                ultimoX = atualX;
                ultimoY = atualY;
                ultimoZ = atualZ;
            }
        }



        void RecarregerTerrenos(int x, int y, int z)
        {
            VerVizinhos(x, y, z);

            for(int a = 0; a < terrenos.Length; a++)
            {
                if (vizinhos.Contains(terrenos[a].ID))
                {
                    terrenos[a].LOD(2);
                    terrenos[a].DesativarParedes();
                }
                else if (vizinhosDistantes.Contains(terrenos[a].ID))
                {
                    terrenos[a].LOD(1);
                    terrenos[a].AtivarParedes(paredes);
                }
                else
                {
                    terrenos[a].LOD(0);
                    terrenos[a].DesativarParedes();
                }
            }

            Debug.Log("Olá, recarregado :)");
        }



        void VerVizinhos(int x, int y, int z)
        {
            vizinhos.Clear();
            vizinhosDistantes.Clear();

            paredes[0] = z + 2;
            paredes[1] = z - 1;
            paredes[2] = x + 2;
            paredes[3] = x - 1;
            paredes[4] = y + 2;
            paredes[5] = y - 1;

            for (int a = -1; a < 3; a++)
            {
                for (int b = -1; b < 3; b++)
                {
                    for (int c = -1; c < 3; c++)
                    {
                        if(a == -1 || b == -1 || c == -1 || a == 2 || b == 2 || c == 2)
                            vizinhosDistantes.Add(new Vector3(x + a, y + b, z + c));
                        else
                             vizinhos.Add(new Vector3(x + a, y + b, z + c));
                    }
                }
            }

        }
    }
}
