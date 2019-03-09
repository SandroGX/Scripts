using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.InventorySystem
{
    public interface IExterior
    {
        void OnExteriorConnect();
        void OnExteriorDisconnect();
    }
}
