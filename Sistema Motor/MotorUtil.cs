using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    public static class MotorUtil
    {

        public static Vector3 MovUniVar(Vector3 velAtual, Vector3 velDesejada, float velMin, float velMax, float acelMin, float acelMax)
        {
            velDesejada = Mathf.Clamp(velDesejada.magnitude, velMin, velMax) * velDesejada.normalized;

            Vector3 acelDes = velDesejada - velAtual;

            acelDes = Mathf.Clamp(acelDes.magnitude, acelMin, acelMax) * acelDes.normalized;

            return velAtual + acelDes;
        }



        public static float MovUniVar(float velAtual, float velDesejada, float velMin, float velMax, float acelMin, float acelMax)
        {
            velDesejada = Mathf.Clamp(Mathf.Abs(velDesejada), velMin, velMax) * Mathf.Sign(velDesejada);
            
            float acelDes = velDesejada - velAtual;

            acelDes = Mathf.Clamp(Mathf.Abs(acelDes), acelMin, acelMax) * Mathf.Sign(acelDes);
            
            return velAtual + acelDes;
        }



        public static Vector3 MovUniVar(Vector3 velAtual, Vector3 velDesejada, float velMin, float velMax, float acelMin, float acelMax, float time)
        {
            return MovUniVar(velAtual, velDesejada * time, velMin * time, velMax * time, acelMin * time * time, acelMax * time * time);
        }



        public static float MovUniVar(float velAtual, float velDesejada, float velMin, float velMax, float acelMin, float acelMax, float time)
        {
            return MovUniVar(velAtual, velDesejada * time, velMin * time, velMax * time, acelMin * time * time, acelMax * time * time);
        }


        public static float GetAnguloComSinal(Vector3 origem, Vector3 destino)
        {
            Vector3 perp = Vector3.Cross(destino, origem).normalized;

            perp.x = Mathf.Abs(perp.x);
            perp.y = Mathf.Abs(perp.y);
            perp.z = Mathf.Abs(perp.z);

            return Vector3.Angle(origem, destino) * Mathf.Sign(Vector3.Dot(destino, Quaternion.AngleAxis(90, perp) * origem));
        }


        public static void NavAgent(Motor motor, MovimentoBasico mB)
        {
            if (motor.navAgent)
            {
                motor.navAgent.speed = mB.velMax;
                motor.navAgent.angularSpeed = mB.velAngMax;
                motor.navAgent.acceleration = mB.acelMax;
            }
        }
    }
}
