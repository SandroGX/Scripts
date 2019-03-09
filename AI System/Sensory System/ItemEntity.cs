using Game.InventorySystem;

namespace Game.AISystem.SensorySystem
{
    public class ItemEntity : ItemComponent, IEntity
    {
        public int ID
        {
            get { return item.GetInstanceID(); }
        }
    }
}
