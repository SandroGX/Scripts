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
        public Vector3 input, lookDir, target;
        [HideInInspector]
        public Vector3 velocity; //in meters/second
        [HideInInspector]
        public Vector3 angularVelocity; //in angles/second
        public Vector3 gravity = Physics.gravity;

        [HideInInspector]
        public bool processedOnce, lookAtTarget;

        public GroundInfo groundInfo;


        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
        } 


        public virtual void FixedUpdate()
        {
            MotorUpdate();
        }


        protected virtual void MotorUpdate()
        {
            OnGround();
            SetLookDir();

            processedOnce = true;

            ApplyMov();

            anim.SetFloat("Velocity", velocity.magnitude);
            anim.SetFloat("Angular Velocity", angularVelocity.y);

            //Debug.DrawRay(transform.position, velocity / Time.fixedDeltaTime, Color.yellow);
        }

        
        protected abstract void ApplyMov();


        public void AnimationEnd()
        {
            
        }


        public void OnGround() {  }


        protected virtual void SetLookDir()
        {
            lookDir = transform.forward;
        }

    }

    public struct Mov
    {
        private Vector3 vel;
        private Vector3 acel;

        public Vector3 Vel{ get { return vel; } set { acel = value - vel;  vel = value; } }
        public Vector3 Acel { get { return acel; } set { acel = value; vel += value; } }

        public Mov(Vector3 vel, Vector3 acel)
        {
            this.vel = vel;
            this.acel = acel;
        }
    }

    public struct GroundInfo
    {
        public bool isGrounded;
        public Vector3 surfaceNormal;
        public Vector3 platformVel;
        public Vector3 platformAngVel;
        public float surfaceStaticFriction;
        public float surfaceDynamicFriction;
    }
}
