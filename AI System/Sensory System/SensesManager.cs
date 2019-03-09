using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.AISystem.SensorySystem
{
    [RequireComponent(typeof(Hitbox))]
    public class SensesManager : MonoBehaviour, IHitboxMessageTarget
    {
        public Sense[] senses;

        private List<StimEvent> events = new List<StimEvent>();
        public event System.Action<StimEvent> OnReceiceEvent;
        public event System.Action<StimEvent> OnRemoveEvent;

        private void Awake()
        {
            senses = GetComponentsInChildren<Sense>();
        }

        private void ReceiveStim(Stim s)
        {
            foreach (Sense sense in senses)
                sense.ReceiveStim(s);

            foreach (StimEvent ev in s.events)
                if (!events.Contains(ev))
                {
                    events.Add(ev);
                    if(OnReceiceEvent != null) OnReceiceEvent(ev);
                }
                else events.Add(ev);
        }

        private void RemoveStim(Stim s)
        {
            foreach (Sense sense in senses)
                sense.RemoveStim(s);

            foreach (StimEvent ev in s.events)
            {
                events.Remove(ev);
                if (OnRemoveEvent != null && !events.Contains(ev)) OnRemoveEvent(ev);
            }
        }

        public int GetScore(StimEvent e)
        {
            int score = 0;

            foreach (Sense s in senses)
                score += s.GetScore(e);

            return score;
        }

        public void OnHitboxEnter(IHitInfo hit)
        {
            if(hit is Stim)
                ReceiveStim(((Stim)hit)); 
        }

        public void OnHitboxExit(IHitInfo hit)
        {
            if (hit is Stim)
                RemoveStim((Stim)hit);
        }
    }
}
