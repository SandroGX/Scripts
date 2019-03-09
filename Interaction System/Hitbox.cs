using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Game
{
    [RequireComponent(typeof(Collider))]
    public class Hitbox : MonoBehaviour
    {
        Collider[] colliders;

        public List<IHitInfo> hits = new List<IHitInfo>();

        public delegate void Hit(IHitInfo hit);
        public event Hit OnHitEnter;
        public event Hit OnHitExit;

        void Awake()
        {
            colliders = GetComponents<Collider>();
        }


        public void OnTriggerEnter(Collider other)
        {
            Hitbox h = other.GetComponent<Hitbox>();
            if (!h) return;
                
            if(h.OnHitEnter != null)
                foreach (IHitInfo hit in hits)
                    h.OnHitEnter(hit);

            if (ExecuteEvents.CanHandleEvent<IHitboxMessageTarget>(h.gameObject))
                foreach (IHitInfo hit in hits)
                    ExecuteEvents.Execute<IHitboxMessageTarget>(h.gameObject, null, (x, y) => x.OnHitboxEnter(hit));
        }


        void OnTriggerExit(Collider other)
        {
            Hitbox h = other.GetComponent<Hitbox>();
            if (!h) return;

            if (h.OnHitExit != null)
                foreach (IHitInfo hit in hits)
                    h.OnHitExit(hit);

            if (ExecuteEvents.CanHandleEvent<IHitboxMessageTarget>(h.gameObject))
                foreach (IHitInfo hit in hits)
                    ExecuteEvents.Execute<IHitboxMessageTarget>(h.gameObject, null, (x, y) => x.OnHitboxExit(hit));
        }


        public void ActivateHitBox(bool active)
        {
            foreach(Collider c in colliders) c.enabled = active; 
        }


        public void Add(IHitInfo hit)
        {
            hit.Other = this;
            hits.Add(hit);
        }

        public void Remove(IHitInfo hit)
        {
            hits.Remove(hit);
        }
    }

}