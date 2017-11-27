using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
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

        public override void ProcessarMovimento(Motor motor)
        {
            float vel = motor.velocidade.magnitude / Time.fixedDeltaTime;
            float velMinAt = (!ignorarVelAnterior && vel < velMin) ? motor.velocidade.magnitude : velMin;
            float velMaxAt = (!ignorarVelAnterior && vel > velMax) ? motor.velocidade.magnitude : velMax;

            Vector3 velDes;

            if (motor.input != Vector3.zero)
                velDes = motor.input * velMaxAt;
            else if(motor.velocidade != Vector3.zero) velDes = motor.velocidade.normalized * velMinAt;
            else velDes = motor.transform.forward * velMinAt;

            motor.velocidade = MotorUtil.MovUniVar(motor.velocidade, velDes, velMinAt, velMaxAt, acelMin, acelMax, Time.fixedDeltaTime);

        }


        public override void Construct(Motor motor)
        {
            base.Construct(motor);

            MotorUtil.NavAgent(motor, this);
        }
    }
}
