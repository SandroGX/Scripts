﻿using UnityEngine;
using System.Collections;
using Game.MotorSystem;

namespace Game.MotorSystem
{
    [CreateAssetMenu(fileName = "Instant Accelaration", menuName = "Motor/Instant Accelaration", order = 1)]
    public class InstantAccelaration : MotorEstado
    {
        public float accelaration = 5;

        public override void ProcessMovement(Motor motor)
        {
            motor.movementVelocity += motor.input * accelaration * Time.fixedDeltaTime;
        }

        public override bool CanStay(Motor motor)
        {
            return !motor.processedOnce || motor.currentState != this;
        }
    }
}