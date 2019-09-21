using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GX.AISystem.SensorySystem
{
    public abstract class Sense : MonoBehaviour
    {
        //private SensesManager man; //not being used

        public Dictionary<Stim, int> stimScores = new Dictionary<Stim, int>(); 

        private void Awake()
        {
            //man = GetComponentInParent<SensesManager>(); //not being used
        }

        public abstract void ReceiveStim(Stim stim);

        public abstract void RemoveStim(Stim stim);

        public int GetScore(StimEvent sEvent)
        {
            int score = 0;

            foreach(Stim stim in stimScores.Keys)
                if (System.Array.Exists(stim.events, x => x = sEvent))
                    score += stimScores[stim];

            return score;
        }
    }


    public abstract class Sense<T> : Sense where T : Stim
    {
        public float updateTime;
        private Coroutine sensing;

        public override void ReceiveStim(Stim stim)
        {
            if (!stim is T) return;

            stimScores.Add(stim, 0);

            if (stimScores.Count == 1) sensing = StartCoroutine(SenseCoroutine());
        }
        
        protected void EvaluateStims()
        {
            foreach(T stim in stimScores.Keys.ToList())
                stimScores[stim] = EvaluateStim(stim);
        }

        protected abstract int EvaluateStim(T stim);

        public IEnumerator SenseCoroutine()
        {
            while (true)
            {
                EvaluateStims();
                yield return new WaitForSeconds(updateTime);
            }
        }

        public override void RemoveStim(Stim stim)
        {
            stimScores.Remove(stim);
            if (stimScores.Count == 0) StopCoroutine(sensing);
        }
    }
}
