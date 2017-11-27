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

        public delegate void A(HitInfo hit);
        public event A OnHitEnter;
        public event A OnHitExit;

        void Awake()
        {
            colliders = GetComponents<Collider>();
        }

        void Start()
        {
            //hit.danos.multiplicadores.Insert(0, 0);
            //ativo = false;
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



        public void Ativar(bool ativo)
        {
            foreach(Collider c in colliders)
                c.enabled = ativo; 
            
        }
    }


    public struct HitInfo
    {
        public Hitbox me;
        public Hitbox other;
        public Danos danos;

    }

}