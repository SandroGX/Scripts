using UnityEngine;
using System.Collections;

namespace Game.AISystem.SensorySystem
{
    public class TestSense : Sense<TestStim>
    {
        public int accuracy;

        protected override int EvaluateStim(TestStim stim)
        {
            int s = accuracy * stim.perceivability;
            Debug.Log("Test Sense: " + s);
            return s;
        }

    }
}
