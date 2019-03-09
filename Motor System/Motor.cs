using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game.MotorSystem
{
    public abstract class Motor : MonoBehaviour
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
        [HideInInspector]
        public Vector3 velocity, movementVelocity, platformVelocity, fallVelocity; //in meters/fixedUpdate
        [HideInInspector]
        public Vector3 angularVelocity, movementAngVelocity, platformAngVelocity; //in angles/fixedUpdate
        public Vector3 gravity = Physics.gravity, surfaceNormal;

        [HideInInspector]
        public bool isStarted, processedOnce, isGrounded, lookAtTarget;
        public float surfaceStaticFriction, surfaceDynamicFriction;


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

            currentState.ProcessMovement(this); processedOnce = true;

            CalculateTotalMov();
            ApplyMov();

            anim.SetFloat("Velocity", velocity.magnitude / Time.fixedDeltaTime);
            anim.SetFloat("Angular Velocity", angularVelocity.y / Time.fixedDeltaTime);
            anim.SetBool("C == N", currentState == nextState);

            //Debug.DrawRay(transform.position, velocity / Time.fixedDeltaTime, Color.yellow);
        }

        protected virtual void CalculateTotalMov()
        {
            velocity = movementVelocity + platformVelocity + fallVelocity;
            angularVelocity = movementAngVelocity + platformAngVelocity;
        }
        protected abstract void ApplyMov();


        public void ChangeState(MotorState state)
        {
            if (!state || currentState == state) return;
            
            currentState.Deconstruct(this);
            currentState = state;
            currentState.Construct(this);
            processedOnce = false;
        }


        public void AnimationEnd()
        {
            
        }


        public virtual void OnGround() { isGrounded = false; surfaceNormal = rawPlatformVelocity = rawPlatformAngVelocity = Vector3.zero; surfaceStaticFriction = surfaceDynamicFriction = 1; }


        protected virtual void SetLookDir()
        {
            lookDir = transform.forward;
        }


        public void InputOnSurface()
        {
            if (surfaceNormal != Vector3.zero)
            {
                Vector3 s = Vector3.ProjectOnPlane(surfaceNormal, Vector3.Cross(input, gravity));
                input = Vector3.ProjectOnPlane(input, s).normalized * input.magnitude;

                //Debug.DrawRay(transform.position, input, Color.red);
            }
        }

    }

    public struct Mov
    {
        Vector3 vel;
        Vector3 acel;

        public Vector3 Vel{ get { return vel; } }
        public Vector3 Acel { set { acel = value; vel += value; } }

        public Mov(Vector3 vel, Vector3 acel)
        {
            this.vel = vel;
            this.acel = acel;
        }

        public static Mov operator+(Mov A, Mov B)
        {
            return new Mov(A.vel + B.vel, A.acel + B.acel);
        }
    }
}
