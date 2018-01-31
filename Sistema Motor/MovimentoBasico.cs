using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Movimento Basico", menuName = "Motor/Movimento Basico", order = 0)]
    public class MovimentoBasico : MotorEstado
    {
        
        public float velMin;
        public float velMax;
        public float acelMin;
        public float acelMax;

        public float velAngMin;
        public float velAngMax;
        public float acelAngMin;
        public float acelAngMax;

        public bool ignorarVelAnterior;

        
        public override void ProcessMovement(Motor motor)
        {
            float vel = motor.movementVelocity.magnitude / Time.fixedDeltaTime;
            float velMinAt = (!ignorarVelAnterior && vel < velMin) ? motor.movementVelocity.magnitude : velMin;
            float velMaxAt = (!ignorarVelAnterior && vel > velMax) ? motor.movementVelocity.magnitude : velMax;

            Vector3 velDes;
            if (motor.input != Vector3.zero) velDes = motor.input * velMaxAt;
            else if(motor.movementVelocity != Vector3.zero) velDes = motor.movementVelocity.normalized * velMinAt;
            else velDes = motor.lookDir * velMinAt;

            motor.velocity = motor.movementVelocity = MotorUtil.MovUniVar(motor.movementVelocity, velDes, velMinAt, velMaxAt, acelMin, acelMax, Time.fixedDeltaTime);

        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);

            MotorUtil.NavAgent(motor, this);
        }


        public override bool CanStay(Motor motor) { return true; }

    }
}
