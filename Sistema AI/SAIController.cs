using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.MotorSystem;
using Game.InventorySystem;

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

            inimigo = GameManager.PLAYER.GetComponent<ItemHolder>().item.GetComponent<Character>();

            ai.root.OnActionEnter(this);
        }


        void Update()
        {
            if (inimigo)
            {
                motor.lookAtTarget = true;
                motor.target = inimigo.item.holder.transform.position;
            }
            else
            {
                motor.lookAtTarget = false;
                inimigo = GameManager.PLAYER.GetComponent<ItemHolder>().item.GetComponent<Character>();
            }
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
