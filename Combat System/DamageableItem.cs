using GX.InventorySystem;

public class DamageableItem : ItemComponent, IDamageable, IExterior
{
    public Statistic Life { get { return life; } }
    public Statistic life;
    

    public override void OnDuplicate()
    {
        life = new Statistic(life);
    }

    public void OnExteriorConnect()
    {
        item.holder.StartCoroutine(life.Variation());
    }

    public void OnExteriorDisconnect()
    {
        item.holder.StopCoroutine(life.Variation());
    }

    public void ReceiveDamage(Damage dam)
    {
        life.Add(-dam.damage);
    }

#if UNITY_EDITOR
    public override void GuiParameters()
    {
        base.GuiParameters();

        if (life) life.Gui();
        else life = new Statistic();
    }
#endif

}
