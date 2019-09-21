using GX;
using GX.InventorySystem;

public class DamagerInterface : ItemComponentInterface<DamagerItem>
{

    public Hitbox hitbox;

    public override void ConnectItem(Item item)
    {
        base.ConnectItem(item);

        hitbox.OnHitEnter += GiveDamage;
    }

    public override void DisconnectItem()
    {
        base.DisconnectItem();

        hitbox.OnHitEnter -= GiveDamage;
    }

    private void GiveDamage(IHitInfo h)
    {
        try
        {
            itemComponent.GiveDamage(((HitInfo<IDamageable>)h).Get());
        }
        finally { }
    }
}
