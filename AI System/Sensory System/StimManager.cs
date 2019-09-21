using UnityEngine;
using System.Collections;

namespace GX.AISystem.SensorySystem
{
    public class StimManager : MonoBehaviour
    {
        public StimEvent[] events;

        public void Awake()
        {
            events = GetComponentsInChildren<StimEvent>();
        }
    }
}
