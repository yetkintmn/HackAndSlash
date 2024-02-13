using DG.Tweening;
using UnityEngine;

public abstract class AttributeBase : MonoBehaviour
{
    [field: SerializeField] public AttributeUI AttributeUI { get; private set; }

    [field: Header("Health")]
    [field: SerializeField] protected int MaxHealth { get; set; }
    [field: SerializeField] protected int Health { get; set; }

    [field: Space(10)]
    [field: SerializeField] protected bool IsDead { get; private set; }

    private Tween _restorationWaitTween;

    protected virtual void LoadAttribute()
    {
        Health = MaxHealth;
        AttributeUI?.SetHealthBar(Health, MaxHealth);
    }

    protected virtual void HealthRestore(int value)
    {
        Health += value;
        if (Health > MaxHealth)
            Health = MaxHealth;
        AttributeUI?.SetHealthBar(Health, MaxHealth);
    }

    public virtual void TakeHit(int value)
    {
        Health -= value;
        if (Health <= 0)
            Die();
        AttributeUI?.SetHealthBar(Health, MaxHealth);
    }

    protected virtual void Die()
    {
        IsDead = true;
    }
}