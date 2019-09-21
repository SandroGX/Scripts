using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GX.MotorSystem
{
    [CreateAssetMenu(fileName = "CCWalk", menuName = "Motor/CharacterController/Walk", order = 0)]
    public class CCWalk : BasicMovement
    {
        public float maxFallSpeed = 35;
        public float staticFriction = 1.5f, dynamicFriction = 1.5f;
        

        public override void ProcessMovement(Motor motor)
        {
            //normal force
            Vector3 normal = motor.groundInfo.surfaceNormal * motor.gravity.magnitude * Mathf.Cos(Vector3.Angle(motor.gravity, -motor.groundInfo.surfaceNormal) * Mathf.Deg2Rad);
            //normal force + gravity
            Vector3 slopeRaw = normal + motor.gravity;
            //friction force
            float friction = normal.magnitude * (motor.velocity == Vector3.zero ? staticFriction * motor.groundInfo.surfaceStaticFriction : dynamicFriction * motor.groundInfo.surfaceDynamicFriction);

            //slope force magnitude counting with friction
            float slopeMag = slopeRaw.magnitude - friction;

            //slope force
            Vector3 slope = slopeMag > 0 ? slopeRaw.normalized * maxFallSpeed : Vector3.zero;
            motor.velocity = MotorUtil.MovUniVarDir(motor.velocity, slope, 0, maxFallSpeed, 0, Mathf.Abs(slopeMag));

            //add platform velocity with friction as max acel
            motor.velocity = MotorUtil.MovUniVarDir(motor.velocity, motor.groundInfo.platformVel, 0, Mathf.Infinity, 0, friction);

            //input direction on surface
            MotorUtil.MotorInputOnSurface(motor);
            base.ProcessMovement(motor);

            //rotate
            float angle = (motor.input != Vector3.zero) ? MotorUtil.GetAngleWithSignal(motor.transform.forward, motor.lookDir) : 0;
            motor.angularVelocity.y = MotorUtil.MovUniVar(motor.angularVelocity.y, angle/Time.fixedDeltaTime, minAngVel, maxAngVel, minAngAcel, maxAngAcel);

            motor.angularVelocity += motor.groundInfo.platformAngVel;


            //motor.transform.up = -cMotor.gravidadeDirecao.normalized; //try putting perpendicular to the ground
        }

        public override void OnStateEnter(Motor motor)
        {
            base.OnStateEnter(motor);
            Vector3 v = Vector3.Project(motor.velocity, motor.gravity);
            motor.velocity -= v;
            v *= Mathf.Cos(Vector3.Angle(motor.gravity, -motor.groundInfo.surfaceNormal) * Mathf.Deg2Rad);
            motor.velocity += v;
        }
    }
}
