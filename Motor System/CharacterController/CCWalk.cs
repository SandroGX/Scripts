﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "CCWalk", menuName = "Motor/CharacterController/Walk", order = 0)]
    public class CCWalk : BasicMovement
    {
        public float maxFallSpeed = 35;
        public float staticFriction = 1.5f, dynamicFriction = 1.5f;
        public int staminaVar;
        public bool allowTired;


        public override void ProcessMovement(Motor motor)
        {
            Vector3 normal = motor.surfaceNormal * motor.gravity.magnitude * Mathf.Cos(Vector3.Angle(motor.gravity, -motor.surfaceNormal) * Mathf.Deg2Rad);
            Vector3 grav = normal + motor.gravity;
            float friction = normal.magnitude * (motor.velocity == Vector3.zero ? staticFriction * motor.surfaceStaticFriction : dynamicFriction * motor.surfaceDynamicFriction);
     
            float acel = grav.magnitude - friction;
            Vector3 v = acel > 0 ? grav.normalized * maxFallSpeed : Vector3.zero;
            motor.fallVelocity = MotorUtil.MovUniVar(motor.fallVelocity, v, 0, maxFallSpeed, 0, Mathf.Abs(acel), Time.fixedDeltaTime);

            motor.platformVelocity = MotorUtil.MovUniVar(motor.platformVelocity, motor.rawPlatformVelocity, 0, Mathf.Infinity, 0, friction * Time.fixedDeltaTime * Time.fixedDeltaTime);

            motor.InputOnSurface();
            base.ProcessMovement(motor);


            float angle = (motor.input != Vector3.zero) ? MotorUtil.GetAngleWithSignal(motor.transform.forward, motor.lookDir) : 0;
            motor.movementAngVelocity.y = MotorUtil.MovUniVar(motor.angularVelocity.y, angle/Time.fixedDeltaTime, minAngVel, maxAngVel, minAngAcel, maxAngAcel, Time.fixedDeltaTime);

            motor.platformAngVelocity = motor.rawPlatformAngVelocity;


            //motor.transform.up = -cMotor.gravidadeDirecao.normalized; //tentar por perpendicular ao chao
        }

        public override void Construct(Motor motor)
        {
            base.Construct(motor);
            motor.character.stamina.varValue -= staminaVar;
            motor.fallVelocity *= Mathf.Cos(Vector3.Angle(motor.gravity, -motor.surfaceNormal) * Mathf.Deg2Rad); 
        }


        public override void Deconstruct(Motor motor)
        {
            base.Deconstruct(motor);
            motor.character.stamina.varValue += staminaVar;
        }
    }
}