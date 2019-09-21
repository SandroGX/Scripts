using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GX.MotorSystem;
using GX.InventorySystem;

namespace GX.AISystem
{
    public class AISController : MonoBehaviour
    {
        public AISAI ai;
        [HideInInspector] //public AISVarSet set;
        Dictionary<AISVariable, AISVariable> set;

        [HideInInspector] public NavMeshAgent navAgent;
        [HideInInspector] public Motor motor;
        

        void Awake()
        {
            motor = GetComponent<Motor>();
            navAgent = GetComponent<NavMeshAgent>();
        }


        void Start()
        {

            if (navAgent)
            {
                navAgent.updatePosition = false;
                navAgent.updateRotation = false;
            }

            ai.Begin(this);
        }


        void Update()
        {
            if (!navAgent || !motor) return;
            Vector3 input = navAgent.desiredVelocity / navAgent.speed;
            motor.input = (input.magnitude > 1) ? input.normalized : input;

            navAgent.nextPosition = transform.position;
        }


        void OnDestroy()
        {
            ai.root.OnActionExit(this);
        }

        public bool ContainsKey(AISVariable key) { return set.ContainsKey(key); }
        public AISVariable GetVar(AISVariable key) { return set[key]; }

        public void MakeSet()
        {
            Dictionary<AISVariable, AISVariable> set = new Dictionary<AISVariable, AISVariable>();

            foreach (AISVariable v in ai.variables)
            {
                if (v.isStatic) set.Add(v, v);
                else
                {
                    AISVariable c = Instantiate(v);
                    c.Init(this);
                    set.Add(v, c);
                }
            }

            this.set = set;
        }
    }
}
