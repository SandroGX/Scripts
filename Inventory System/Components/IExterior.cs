using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.InventorySystem
{
    public interface IExterior
    {
        void OnExteriorConnect();
        void OnExteriorDisconnect();
    }
}
