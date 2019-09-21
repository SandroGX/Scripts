using UnityEngine;
using System.Collections;

namespace GX.AISystem.SensorySystem
{
    public class GameObjectEntity : MonoBehaviour, IEntity
    {
        public int ID
        {
            get { return gameObject.GetInstanceID(); }
        }
    }
}
