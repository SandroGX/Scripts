using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Game.AISystem
{
    public class PatrolAgent : MonoBehaviour
    {
        public AISPatrol[] patrols;
        public int crrPatrol;
        private NavMeshAgent nav;
        Transform target;

        private void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            
        }

        public Transform Next(int crrPatrol)
        {
            return patrols[crrPatrol].Next(this);
        }

        public bool HasNext(int crrPatrol)
        {
            return patrols[crrPatrol].HasNext(this);
        }

        public void Subscribe(int crrPatrol)
        {
            patrols[crrPatrol].Subscribe(this);
        }

        public void Unsubscribe(int crrPatrol)
        {
            patrols[crrPatrol].Unsubscribe(this);
        }
    }
}
