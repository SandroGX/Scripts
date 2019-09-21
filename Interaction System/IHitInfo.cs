using UnityEngine;
using UnityEditor;

namespace GX
{
    public interface IHitInfo
    {
        Hitbox Me { get; set; }
        Hitbox Other { get; set; }
    }
}