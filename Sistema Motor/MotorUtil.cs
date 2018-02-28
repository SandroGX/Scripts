using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    public static class MotorUtil
    {

        public static Vector3 MovUniVar(Vector3 crrVel, Vector3 desVel, float minVel, float maxVel, float minAcel, float maxAcel)
        {
            desVel = Mathf.Clamp(desVel.magnitude, minVel, maxVel) * desVel.normalized;

            Vector3 acelDes = desVel - crrVel;

            acelDes = Mathf.Clamp(acelDes.magnitude, minAcel, maxAcel) * acelDes.normalized;

            return crrVel + acelDes;
        }

        public static float MovUniVar(float crrVel, float desVel, float minVel, float maxVel, float minAcel, float maxAcel)
        {
            desVel = Mathf.Clamp(Mathf.Abs(desVel), minVel, maxVel) * Mathf.Sign(desVel);
            
            float acelDes = desVel - crrVel;

            acelDes = Mathf.Clamp(Mathf.Abs(acelDes), minAcel, maxAcel) * Mathf.Sign(acelDes);
            
            return crrVel + acelDes;
        }

        public static Vector3 MovUniVar(Vector3 crrVel, Vector3 desVel, float minVel, float maxVel, float minAcel, float maxAcel, float time)
        {
            return MovUniVar(crrVel, desVel * time, minVel * time, maxVel * time, minAcel * time * time, maxAcel * time * time);
        }

        public static float MovUniVar(float crrVel, float desVel, float minVel, float maxVel, float minAcel, float maxAcel, float time)
        {
            return MovUniVar(crrVel, desVel * time, minVel * time, maxVel * time, minAcel * time * time, maxAcel * time * time);
        }

   
        public static float GetAngleWithSignal(Vector3 origin, Vector3 destiny)
        {
            Vector3 perp = Vector3.Cross(destiny, origin).normalized;

            perp.x = Mathf.Abs(perp.x);
            perp.y = Mathf.Abs(perp.y);
            perp.z = Mathf.Abs(perp.z);

            return Vector3.Angle(origin, destiny) * Mathf.Sign(Vector3.Dot(destiny, Quaternion.AngleAxis(90, perp) * origin));
        }


        public static void NavAgent(Motor motor, BasicMovement bM)
        {
            if (motor.navAgent)
            {
                motor.navAgent.speed = bM.maxVel;
                motor.navAgent.angularSpeed = bM.maxAngVel;
                motor.navAgent.acceleration = bM.maxAcel;
            }
        }


        public static Vector3 KillVector(Vector3 toKill, Vector3 killer)
        {
            return toKill - Vector3.Project(toKill, killer);
        }

        public static void KillVelocities(Motor motor, Vector3 killer)
        {
            motor.movementVelocity = KillVector(motor.movementVelocity, killer);
            motor.platformVelocity = KillVector(motor.platformVelocity, killer);
            motor.fallVelocity = KillVector(motor.fallVelocity, killer);
        }
    }
}
