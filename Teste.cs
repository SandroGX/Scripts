using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Game.SistemaMotor;

public class Teste : MonoBehaviour {

	void Start ()
    {
        Assembly ass = Assembly.GetExecutingAssembly();

        IEnumerable<Type> t = ass.GetTypes().Where(a => a != typeof(MotorEstado) && typeof(MotorEstado).IsAssignableFrom(a));

        foreach (Type s in t)
        {
            Debug.Log(s.Name);
        }
	}

}
 