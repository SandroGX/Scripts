using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.SistemaMotor
{
    public class CCMotor : Motor
    {
        [HideInInspector]
        public CharacterController controller;

        public RaycastHit floorHit;
        [HideInInspector]
        public bool isGrounded;
        public float groundMinDistance = 0.2f;
        public Vector3 LookDir { get; set; }
        public Vector3 Target { get; set; }

        public Vector3 gravity = Physics.gravity;

        protected override void Awake()
        {
            base.Awake();
            controller = GetComponent<CharacterController>();
        }


        protected override void MotorUpdate()
        {
            isGrounded = OnGround();

            base.MotorUpdate();

            controller.Move(velocity);
            transform.eulerAngles += angularVelocity;
        }


        public override bool OnGround()
        {
            return Physics.Raycast(transform.position, gravity, out floorHit, groundMinDistance);
        }

    }
}
