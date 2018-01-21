using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.SistemaMotor;
using Game.SistemaInventario;

namespace Game.SistemaAI
{
    public class SAIController : MonoBehaviour
    {
        public SAIAI ai;

        [HideInInspector]
        public  NavMeshAgent navAgent;
        [HideInInspector]
        public Motor motor;
        [HideInInspector]
        public Character character;

        public Character inimigo;

        void Awake()
        {
        
            navAgent = GetComponent<NavMeshAgent>();
            motor = GetComponent<Motor>();

        }


        void Start()
        {
            character = GetComponent<ItemHolder>().item.GetComponent<Character>();
            motor.MotorStart();
            navAgent.updatePosition = false;
            navAgent.updateRotation = false;

            inimigo = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHolder>().item.GetComponent<Character>();

            ai.root.OnActionEnter(this);
        }


        void Update()
        {
            ((CCMotor)motor).Target = inimigo ? inimigo.item.holder.transform.position : transform.forward;
            Vector3 input = navAgent.desiredVelocity / navAgent.speed;
            motor.input = (input.magnitude > 1) ? input.normalized : input;

            navAgent.nextPosition = transform.position;
        }

        private void OnDestroy()
        {
            ai.root.OnActionExit(this);
        }
    }
}
