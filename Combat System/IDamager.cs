public interface IDamager
{
    Damage DamageToGive { get; }

    void GiveDamage(IDamageable d);
}
