using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game.MotorSystem
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
        public Character character;

        [HideInInspector]
        public Vector3 input, lookDir, target, rawPlatformVelocity, rawPlatformAngVelocity;
        //[HideInInspector]
        public Vector3 velocity, movementVelocity, platformVelocity, fallVelocity; //em metros/fixedUpdate
        [HideInInspector]
        public Vector3 angularVelocity, movementAngVelocity, platformAngVelocity; //em angulos/fixedUpdate
        public Vector3 gravity = Physics.gravity, surfaceNormal;
        [HideInInspector]
        public bool isStarted, processedOnce, isGrounded, lookAtTarget;
        public float surfaceStaticFriction, surfaceDynamicFriction;

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
        } 


        public virtual void MotorStart()
        {
            character = GetComponent<InventorySystem.ItemHolder>().item.GetComponent<Character>();

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
            OnGround();
            SetLookDir();
            ChangeState(currentState.GetNextState(this));
            currentState.ProcessMovement(this); processedOnce = true;
            velocity = movementVelocity + platformVelocity + fallVelocity;
            angularVelocity = movementAngVelocity + platformAngVelocity;
            anim.SetFloat("Velocidade", velocity.magnitude / Time.fixedDeltaTime);
            anim.SetFloat("Velocidade Angular", angularVelocity.y / Time.fixedDeltaTime);
            anim.SetBool("C == N", currentState == nextState);

            //Debug.DrawRay(transform.position, velocity / Time.fixedDeltaTime, Color.yellow);
        }


        public void ChangeState(MotorEstado state)
        {
            if (!state || currentState == state) return;
            
            currentState.Deconstruct(this);
            currentState = state;
            currentState.Construct(this);
            processedOnce = false;
            ChangeState(currentState.GetNextState(this));
        }


        public void DefaultEstado()
        {
            nextState = defaultState;
            ChangeState(defaultState);
        }


        public void AnimationEnd()
        {
            currentState.OnAnimationEnd(this);
        }


        public virtual void OnGround() { isGrounded = false; surfaceNormal = rawPlatformVelocity = rawPlatformAngVelocity = Vector3.zero; surfaceStaticFriction = surfaceDynamicFriction = 1; }


        protected virtual void SetLookDir()
        {
            lookDir = transform.forward;
        }


        public void InputOnSurface()
        {
            if (surfaceNormal != Vector3.zero) input = Vector3.ProjectOnPlane(input, surfaceNormal);
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
                if (a.Contains(n)) acao(a);
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
