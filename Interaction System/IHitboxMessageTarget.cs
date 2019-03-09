using UnityEngine.EventSystems;
using UnityEditor;

namespace Game
{
    public interface IHitboxMessageTarget : IEventSystemHandler
    {
        void OnHitboxEnter(IHitInfo hit);
        void OnHitboxExit(IHitInfo hit);
    }
}