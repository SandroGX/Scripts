using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MotorSystem
{
    public static class MotorUtil
    {
        public static Mov MovUniVar(Mov crrMov, Vector3 desVel, float minVel, float maxVel, float minAcel, float maxAcel)
        {
            desVel = Mathf.Clamp(desVel.magnitude, minVel, maxVel) * desVel.normalized;

            Vector3 acelDes = desVel - crrMov.Vel;

            acelDes = Mathf.Clamp(acelDes.magnitude, minAcel, maxAcel) * acelDes.normalized;

            crrMov.Acel = acelDes;

            return crrMov;
        }

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

        public static Vector3 MovUniVarDir(Vector3 crrVel, Vector3 desVel, float minVel, float maxVel, float minAcel, float maxAcel)
        {
            Vector3 dir = Vector3.Project(crrVel, desVel);

            crrVel -= dir;
            return crrVel + MovUniVar(dir, desVel, minVel, maxVel, minAcel, maxAcel);
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

        public static Vector3 InputOnSurface(Vector3 input, Vector3 surfaceNormal, Vector3 gravity)
        {
            if (surfaceNormal != Vector3.zero)
            {
                Vector3 s = Vector3.ProjectOnPlane(surfaceNormal, Vector3.Cross(input, gravity));
                input = Vector3.ProjectOnPlane(input, s).normalized * input.magnitude;
            }

            return input;
        }

        public static void MotorInputOnSurface(Motor motor)
        {
            motor.input = InputOnSurface(motor.input, motor.groundInfo.surfaceNormal, motor.gravity);
        }
    }
}
