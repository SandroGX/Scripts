using UnityEngine;
using UnityEditor;

namespace GX.AISystem
{
    public abstract class AISPatrol : MonoBehaviour
    {
        public abstract void Subscribe(PatrolAgent agent);
        public abstract void Unsubscribe(PatrolAgent agent);
        public abstract Transform Next(PatrolAgent agent);
        public abstract bool HasNext(PatrolAgent agent);
    }
}