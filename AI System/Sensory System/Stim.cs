using UnityEngine;

namespace Game.AISystem.SensorySystem
{
    public abstract class Stim : MonoBehaviour, IHitInfo
    {
        public Hitbox Me { get; set; }
        public Hitbox Other { get; set; }

        public Hitbox detectBoundary;

        StimManager man;
        public StimEvent[] events;

        private void Awake()
        {
            man = GetComponentInParent<StimManager>();
        }

        private void OnEnable()
        {
            detectBoundary.Add(this);
        }

        private void OnDisable()
        {
            detectBoundary.Remove(this);
        }
    }
}
