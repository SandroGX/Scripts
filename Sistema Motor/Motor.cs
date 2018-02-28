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
        public Vector3 velocity, movementVelocity, platformVelocity, fallVelocity; //in meters/fixedUpdate
        [HideInInspector]
        public Vector3 angularVelocity, movementAngVelocity, platformAngVelocity; //in angles/fixedUpdate
        public Vector3 gravity = Physics.gravity, surfaceNormal;
        [HideInInspector]
        public bool isStarted, processedOnce, isGrounded, lookAtTarget;
        public float surfaceStaticFriction, surfaceDynamicFriction;

        public Dictionary<string, IActivatable> activatables = new Dictionary<string, IActivatable>();

        public MotorState currentState;
        public MotorState nextState;
        [SerializeField]
        public MotorState defaultState;


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
            anim.SetFloat("Velocity", velocity.magnitude / Time.fixedDeltaTime);
            anim.SetFloat("Angular Velocity", angularVelocity.y / Time.fixedDeltaTime);
            anim.SetBool("C == N", currentState == nextState);

            //Debug.DrawRay(transform.position, velocity / Time.fixedDeltaTime, Color.yellow);
        }


        public void ChangeState(MotorState state)
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
            activatables[toActivate].Activate(true);
        }

        public void Deactivate(string toDeactivate)
        {
            activatables[toDeactivate].Activate(false);
        }


        public void GetAtivatablesWithName(string n, Action<string> toDo)
        {
            foreach (string a in activatables.Keys)
            {
                if (a.Contains(n)) toDo(a);
            }
        }

        public void ActivateAllWithName(string toActivate)
        {
            GetAtivatablesWithName(toActivate, x => activatables[x].Activate(true));
        }

        public void DeactivateAllWithName(string toDeactivate)
        {
            GetAtivatablesWithName(toDeactivate, x => activatables[x].Activate(false));

        }

    }
}
