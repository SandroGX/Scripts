using UnityEngine;
using UnityEditor;

namespace Game
{
    public interface IHitInfo
    {
        Hitbox Me { get; set; }
        Hitbox Other { get; set; }
    }
}