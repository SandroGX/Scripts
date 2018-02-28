using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.MotorSystem;
using Game.InventorySystem;

namespace Game.AISystem
{
    public class AISController : MonoBehaviour
    {
        public AISAI ai;

        [HideInInspector] public NavMeshAgent navAgent;
        [HideInInspector] public Motor motor;
        public List<AISVariable> variables;
        

        void Awake()
        {
            motor = GetComponent<Motor>();
            navAgent = GetComponent<NavMeshAgent>();
            Debug.Log("AAA");
            SetAIVars(variables);
        }


        void Start()
        {
            if (motor) motor.MotorStart();
            if (navAgent)
            {
                navAgent.updatePosition = false;
                navAgent.updateRotation = false;
            }

            ai.root.OnActionEnter(this);
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


        public void SetAIVars(List<AISVariable> original)
        {
            List<AISVariable> n = new List<AISVariable>();

            foreach (AISVariable v in original)
            {
                if (v.isStatic) n.Add(v);
                else
                {
                    AISVariable c = Instantiate(v);
                    n.Add(c);
                }
            }

            variables = n;
        }


        public AISVarSingle GetSingle(int idx) { return variables[idx] as AISVarSingle; }
        public AISVarList GetList(int idx) { return variables[idx] as AISVarList; }
    }
}
