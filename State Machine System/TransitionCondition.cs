﻿using UnityEngine;
using UnityEditor;

namespace GX.StateMachineSystem
{
    //a condition of a transition
    public abstract class TransitionCondition : ScriptableObject
    {
        //is condition met
        public abstract bool ConditionMet(SMClient client);

        //start evaluating?


        //stop evaluating?

    }
}