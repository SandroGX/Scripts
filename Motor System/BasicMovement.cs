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
            float vel = motor.velocity.magnitude / Time.fixedDeltaTime;
            float crrMinVel = (!ignorePrevVel && vel < minVel) ? vel : minVel;
            float crrMaxVel = (!ignorePrevVel && vel > maxVel) ? vel : maxVel;

            Vector3 velDes;
            if (motor.input != Vector3.zero) velDes = motor.input * crrMaxVel;
            else if(motor.velocity != Vector3.zero) velDes = motor.velocity.normalized * crrMinVel;
            else velDes = motor.lookDir * crrMinVel;

            motor.velocity = MotorUtil.MovUniVar(motor.velocity, velDes, crrMinVel, crrMaxVel, minAcel, maxAcel);
        }


        public override void OnStateEnter(Motor motor)
        {
            MotorUtil.NavAgent(motor, this);
        }

        public override void OnStateExit(Motor motor)
        {
            throw new System.NotImplementedException();
        }
    }
}
