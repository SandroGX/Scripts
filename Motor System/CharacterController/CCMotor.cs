using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GX.MotorSystem
{
    public class CCMotor : Motor
    {
        [HideInInspector]
        public CharacterController controller;

        public RaycastHit floorHit;
        [HideInInspector]
        public float groundMinDistance = 0.2f;


        protected override void Awake()
        {
            base.Awake();
            controller = GetComponent<CharacterController>();
        }


        protected override void ApplyMov()
        {
            controller.Move(velocity);
            transform.eulerAngles += angularVelocity;
        }


        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            MotorUtil.KillVector(velocity, hit.normal);
        }


        protected override void SetLookDir()
        {
            lookDir = (lookAtTarget ? target - transform.position : (input != Vector3.zero ? input : transform.forward)).normalized;

            //if (surfaceNormal != Vector3.zero)
                lookDir = Vector3.ProjectOnPlane(lookDir, gravity).normalized;

            //Debug.DrawRay(transform.position, lookDir);
        }

    }
}
