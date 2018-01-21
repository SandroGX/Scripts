using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "Andar", menuName = "Motor/CharacterController/Andar", order = 0)]
    public class CCAndar : MovimentoBasico
    {
        public int staminaVar;
        public int staminaExit;


        public override void ProcessMovement(Motor motor)
        {
            CCMotor cMotor = (CCMotor)motor;

            //Vector3 gravity = Vector3.Cross(cMotor.gravity, cMotor.floorHit.normal).normalized * cMotor.gravity.magnitude * Mathf.Sin(Vector3.Angle(cMotor.gravity, cMotor.floorHit.normal));
            //motor.fallVelocity = MotorUtil.MovUniVar(motor.fallVelocity, )

            Vector3 a = motor.platformVelocity;
            motor.platformVelocity = cMotor.floorHit.rigidbody ? cMotor.floorHit.rigidbody.GetPointVelocity(cMotor.floorHit.point) * Time.fixedDeltaTime : Vector3.zero;

            base.ProcessMovement(motor);
            cMotor.movementVelocity = Vector3.ProjectOnPlane(motor.movementVelocity, cMotor.floorHit.normal).normalized * motor.movementVelocity.magnitude;


            cMotor.LookDir = Vector3.ProjectOnPlane(cMotor.Target - cMotor.transform.position, cMotor.gravity).normalized;
            float angle = (motor.input != Vector3.zero) ? MotorUtil.GetAnguloComSinal(motor.transform.forward, cMotor.LookDir) : 0;

            motor.movementAngVelocity.y = MotorUtil.MovUniVar(motor.angularVelocity.y, angle/Time.fixedDeltaTime, velAngMin, velAngMax, acelAngMin, acelAngMax, Time.fixedDeltaTime);
            //motor.platformAngVelocity.y = motor.platformVelocity != Vector3.zero ? MotorUtil.GetAnguloComSinal(new Vector3(a.x, 0, a.z), new Vector3(motor.platformVelocity.x, 0, motor.platformVelocity.z)) : 0; 
            //tentar acompanhar a rot da platforma

            //motor.transform.up = -cMotor.gravidadeDirecao.normalized; //tentar por perpendicular ao chao
        }

        public override void Construct(Motor motor)
        {
            motor.character.stamina.varValue -= staminaVar;
            motor.fallVelocity = Vector3.zero;
        }


        public override void Deconstruct(Motor motor)
        {
            motor.character.stamina.varValue += staminaVar;
        }


        public override bool CanStay(Motor motor)
        {
            return ((CCMotor)motor).isGrounded && motor.character.stamina.value >= staminaExit;
        }

    }
}
