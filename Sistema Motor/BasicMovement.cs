using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Basic Movement", menuName = "Motor/Basic Movement", order = 0)]
    public class BasicMovement : MotorState
    {
        
        public float minVel;
        public float maxVel;
        public float minAcel;
        public float maxAcel;

        public float minAngVel;
        public float maxAngVel;
        public float minAngAcel;
        public float maxAngAcel;

        public bool ignorePrevVel;

        
        public override void ProcessMovement(Motor motor)
        {
            float vel = motor.movementVelocity.magnitude / Time.fixedDeltaTime;
            float crrMinVel = (!ignorePrevVel && vel < minVel) ? motor.movementVelocity.magnitude : minVel;
            float crrMaxVel = (!ignorePrevVel && vel > maxVel) ? motor.movementVelocity.magnitude : maxVel;

            Vector3 velDes;
            if (motor.input != Vector3.zero) velDes = motor.input * crrMaxVel;
            else if(motor.movementVelocity != Vector3.zero) velDes = motor.movementVelocity.normalized * crrMinVel;
            else velDes = motor.lookDir * crrMinVel;

            motor.velocity = motor.movementVelocity = MotorUtil.MovUniVar(motor.movementVelocity, velDes, crrMinVel, crrMaxVel, minAcel, maxAcel, Time.fixedDeltaTime);

        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);

            MotorUtil.NavAgent(motor, this);
        }


        public override bool CanStay(Motor motor) { return true; }

    }
}
