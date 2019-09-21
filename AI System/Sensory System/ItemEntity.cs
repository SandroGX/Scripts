using GX.InventorySystem;

namespace GX.AISystem.SensorySystem
{
    public class ItemEntity : ItemComponent, IEntity
    {
        public int ID
        {
            get { return item.GetInstanceID(); }
        }
    }
}
