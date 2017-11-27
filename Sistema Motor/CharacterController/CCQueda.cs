using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaMotor
{
    [CreateAssetMenu(fileName = "Queda", menuName = "Motor/CharacterController/Queda", order = 1)]
    public class CCQueda : MovimentoBasico
    {

        public float velocidadeTerminal = 15;

        public override void ProcessarMovimento(Motor motor)
        {
            CCMotor cMotor = (CCMotor)motor;

            Vector3 g = Vector3.Project(motor.velocidade, cMotor.gravidadeDirecao);

            motor.velocidade -= g;

            base.ProcessarMovimento(motor);

            motor.velocidade += MotorUtil.MovUniVar(g, cMotor.gravidadeDirecao * velocidadeTerminal, 1, velocidadeTerminal, 0, cMotor.Gravidade, Time.fixedDeltaTime);

        }


        public override void Deconstruct(Motor motor)
        {
            base.Deconstruct(motor);

            CCMotor cMotor = (CCMotor)motor;

            Vector3 i = Vector3.Project(motor.velocidade, cMotor.gravidadeDirecao);

            motor.velocidade -= i;
        }


        public override void Transicao(Motor motor)
        {
            if (motor.NoChao())
                motor.DefaultEstado();
        }

    }
}
