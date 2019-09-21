using UnityEngine;
using GX;

public class DamagerBehaviour : MonoBehaviour, IDamager
{
    public Damage DamageToGive { get { return damage; } }
    public Damage damage;

    public Hitbox hitbox;

    private void Awake()
    {
        hitbox.OnHitEnter += GiveDamage;
    }

    public void GiveDamage(IDamageable d)
    {
        d.ReceiveDamage(DamageToGive);
    }

    private void GiveDamage(IHitInfo h)
    {
        try
        {
            GiveDamage(((HitInfo<IDamageable>)h).Get());
        }
        finally { }
    }
}
