using GX;
using GX.InventorySystem;

public class DamageableItemInterface : ItemComponentInterface<DamageableItem>
{

    public Hitbox hitbox;
    public HitInfo<IDamageable> hit;


    public override void ConnectItem(Item item)
    {
        base.ConnectItem(item);

        hit = new HitInfo<IDamageable>(itemComponent);
        hitbox.Add(hit);
    }

    public override void DisconnectItem()
    {
        base.DisconnectItem();

        hitbox.Remove(hit);
    }
}
