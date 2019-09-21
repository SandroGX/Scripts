using UnityEngine.EventSystems;
using UnityEditor;

namespace GX
{
    public interface IHitboxMessageTarget : IEventSystemHandler
    {
        void OnHitboxEnter(IHitInfo hit);
        void OnHitboxExit(IHitInfo hit);
    }
}