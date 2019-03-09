public interface IDamageable
{
    Statistic Life { get; }
    void ReceiveDamage(Damage dam);
} 