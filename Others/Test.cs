using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);

        g.transform.SetParent(transform);

        GetComponent<Animator>().Rebind();
	}
}
