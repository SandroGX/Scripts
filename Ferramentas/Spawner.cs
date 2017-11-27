#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Spawner : MonoBehaviour
{
    public int numero;
    public GameObject prefab;
    public Vector2 dimensoes = new Vector2(2, 2);
    public float distanciaMinimadeDeOutros = 5;
    public float anguloMax = 30;
    public bool seguirChaoNormal = true;
    public Vector2 rotVarY = new Vector2(0, 360);
    public float offsetDoChao;
    public LayerMask ondeGerar, ondeNaoGerar;

    int tentativasMax = 70;


    List<RaycastHit> pontos = new List<RaycastHit>();


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(dimensoes.x, 0.1f, dimensoes.y));
    }

    void PontosAoAcaso()
    {
        for (int e = 0; e < numero; e++)
        {
            for (int i = 0; i < tentativasMax; i++)
            {
                Vector3 a = transform.rotation * new Vector3(Random.Range(-dimensoes.x / 2, dimensoes.x / 2), 0, Random.Range(-dimensoes.y / 2, dimensoes.y / 2)) + transform.position;

                RaycastHit hit;

                if (!Physics.Raycast(a, -transform.up, out hit, Mathf.Infinity, ondeGerar, QueryTriggerInteraction.Ignore))
                    continue;

                if (Physics.Raycast(a, -transform.up, out hit, Mathf.Infinity, ondeNaoGerar, QueryTriggerInteraction.Ignore))
                    continue;

                if (90 - Vector3.Angle(transform.up, hit.normal) < anguloMax)
                    continue;

                bool b = false;

                foreach (Vector3 f in pontos.Select(x => x.point))
                {
                    if (Vector3.Distance(hit.point, f) < distanciaMinimadeDeOutros)
                    {
                        b = true;
                        break;
                    }
                }

                if (b)
                    continue;

                pontos.Add(hit);
                break;
            }
        }

    }

    public void Gerar()
    {
        pontos.Clear();

        LimparFilhos();

        PontosAoAcaso();

        foreach (RaycastHit hit in pontos)
        {
            GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            go.transform.parent = transform;
            go.transform.position = hit.point - transform.up * offsetDoChao;

            if(seguirChaoNormal)
            {
                go.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation; ;
            }

            go.transform.eulerAngles += Vector3.up * Random.Range(rotVarY.x, rotVarY.y);
        }
    }


    public void LimparFilhos()
    {
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }


}
#endif
