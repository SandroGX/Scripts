using UnityEngine;
using System.Collections;

namespace Game.AISystem.SensorySystem
{
    public class GameObjectEntity : MonoBehaviour, IEntity
    {
        public int ID
        {
            get { return gameObject.GetInstanceID(); }
        }
    }
}
