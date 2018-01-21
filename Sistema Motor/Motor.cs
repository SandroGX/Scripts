using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game.SistemaMotor
{

    public class Motor : MonoBehaviour
    {
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public NavMeshAgent navAgent;
        [HideInInspector]
        public Rigidbody rigidBody;
        [HideInInspector]
        public CharacterController characterController;
        [HideInInspector]
        public Character character;

        //[HideInInspector]
        public Vector3 velocity, movementVelocity, platformVelocity, fallVelocity; //em metros/fixedUpdate
        [HideInInspector]
        public Vector3 angularVelocity, movementAngVelocity, platformAngVelocity; //em angulos/fixedUpdate
        [HideInInspector]
        public Vector3 input;
        [HideInInspector]
        public bool isStarted;

        public Dictionary<string, IAtivavel> ativaveis = new Dictionary<string, IAtivavel>();

        public MotorEstado currentState;
        public MotorEstado nextState;
        [SerializeField]
        public MotorEstado defaultState;


        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            rigidBody = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();

        } 


        public virtual void MotorStart()
        {
            character = GetComponent<SistemaInventario.ItemHolder>().item.GetComponent<Character>();

            isStarted = true;

            currentState = defaultState;
            currentState.Construct(this);
        }


        public virtual void FixedUpdate()
        {
            if (isStarted) MotorUpdate();
        }


        protected virtual void MotorUpdate()
        {
            MudarEstado(currentState.Transition(this));
            currentState.ProcessMovement(this);
            velocity = movementVelocity + platformVelocity + fallVelocity;
            angularVelocity = movementAngVelocity + platformAngVelocity;
            anim.SetFloat("Velocidade", velocity.magnitude / Time.fixedDeltaTime);
            anim.SetFloat("Velocidade Angular", angularVelocity.y / Time.fixedDeltaTime);
            anim.SetBool("C == N", currentState == nextState);
        }


        public virtual bool OnGround() { return false; }


        public void MudarEstado(MotorEstado state)
        {
            if (!state || currentState == state) return;
            
            currentState.Deconstruct(this);
            currentState = state;
            currentState.Construct(this);
            MudarEstado(currentState.Transition(this));
        }


        public void DefaultEstado()
        {
            nextState = defaultState;
            MudarEstado(defaultState);
        }


        public void AnimacaoEnd()
        {
            currentState.OnAnimationEnd(this);
        }

        

        public void Activate(string toActivate)
        {
            ativaveis[toActivate].Activate(true);
        }

        public void Deactivate(string toDeactivate)
        {
            ativaveis[toDeactivate].Activate(false);
        }



        public void GetAtivaveisComNome(string n, Action<string> acao)
        {
            foreach (string a in ativaveis.Keys)
            {
                if (a.Contains(n))
                    acao(a);
            }
        }



        public void AtivarTodosComNome(string aAtivar)
        {
            GetAtivaveisComNome(aAtivar, x => ativaveis[x].Activate(true));
        }



        public void DesativarTodosComNome(string aDesativar)
        {
            GetAtivaveisComNome(aDesativar, x => ativaveis[x].Activate(false));

        }

    }
}
