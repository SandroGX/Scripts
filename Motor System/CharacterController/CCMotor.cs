using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.MotorSystem
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


        public override void OnGround()
        {
            float radius = controller.radius;

            if (Physics.Raycast(transform.position, gravity, out floorHit, groundMinDistance) ||
               Physics.Raycast(transform.position + (transform.up + -transform.forward) * radius, gravity, out floorHit, groundMinDistance + radius) ||
               Physics.Raycast(transform.position + (transform.up + transform.forward) * radius, gravity, out floorHit, groundMinDistance + radius) ||
               Physics.Raycast(transform.position + (transform.up + transform.right) * radius, gravity, out floorHit, groundMinDistance + radius) ||
               Physics.Raycast(transform.position + (transform.up + -transform.right) * radius, gravity, out floorHit, groundMinDistance + radius))
            {
                isGrounded = true;
                surfaceNormal = floorHit.normal;
                if (floorHit.rigidbody)
                {
                    rawPlatformVelocity = floorHit.rigidbody.GetPointVelocity(floorHit.point) * Time.fixedDeltaTime;
                    rawPlatformAngVelocity = floorHit.rigidbody.angularVelocity * Mathf.Rad2Deg * Time.fixedDeltaTime;
                }
                else rawPlatformVelocity = rawPlatformAngVelocity = Vector3.zero;
                if (floorHit.collider.material)
                {
                    surfaceStaticFriction = floorHit.collider.material.staticFriction;
                    surfaceDynamicFriction = floorHit.collider.material.dynamicFriction;
                }
                else surfaceStaticFriction = surfaceDynamicFriction = 1;
            }
            else base.OnGround();
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            MotorUtil.KillVelocities(this, hit.normal);
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
