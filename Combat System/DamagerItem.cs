using GX.InventorySystem;

[System.Serializable]
public class DamagerItem : ItemComponent, IDamager
{
    public Damage DamageToGive { get { return damage; } }
    public Damage damage;

    public void GiveDamage(IDamageable d)
    {
        d.ReceiveDamage(damage);
    }

#if UNITY_EDITOR

    public override void GuiParameters()
    {
        base.GuiParameters();
        damage.Gui();
    }
#endif
}