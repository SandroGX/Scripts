using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider))]
    public class Hitbox : MonoBehaviour//, IAtivavel
    {
        Collider[] colliders;

        public HitInfo hit = new HitInfo();

        public delegate void Hit(HitInfo hit);
        public event Hit OnHitEnter;
        public event Hit OnHitExit;

        void Awake()
        {
            colliders = GetComponents<Collider>();
        }


        void Start()
        {
            hit.other = this;
        }


        void OnTriggerEnter(Collider other)
        {
            Hitbox h = other.GetComponent<Hitbox>();
            if (h && h.OnHitEnter != null)
            {
                hit.me = h;
                h.OnHitEnter(hit);
            }
        }


        void OnTriggerExit(Collider other)
        {
            Hitbox h = other.GetComponent<Hitbox>();
            if (h && h.OnHitExit != null)
            {
                hit.me = h;
                h.OnHitExit(hit);
            }
        }



        public void ActivateHitBox(bool active)
        {
            foreach(Collider c in colliders) c.enabled = active; 
        }
    }


    public struct HitInfo
    {
        public Hitbox me;
        public Hitbox other;
        public Damage damage;

    }

}