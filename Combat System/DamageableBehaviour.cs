using UnityEngine;
using Game;

public class DamageableBehaviour : MonoBehaviour, IDamageable
{
    public Statistic Life { get { return life; } }
    public Statistic life;

    public Hitbox hitbox;
    public HitInfo<IDamageable> hit;

    public void ReceiveDamage(Damage dam)
    {
        life.Add(-dam.damage);
    }

    private void Awake()
    {
        StartCoroutine(life.Variation());
        hit = new HitInfo<IDamageable>(this);
        hitbox.Add(hit);
    }
}
