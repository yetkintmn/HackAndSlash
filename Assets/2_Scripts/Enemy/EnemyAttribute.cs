using DG.Tweening;
using System;
using TMN.PoolManager;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttribute : AttributeBase
{
    [SerializeField] private ParticleSystem takeHitParticle;

    public UnityAction TakeHitAction;
    public UnityAction DeadAction;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        LoadAttribute();
    }

    public override void TakeHit(int value)
    {
        base.TakeHit(value);
        if (IsDead)
            return;
        ShakeBody();
        CollisionTimer();
        TakeHitAction?.Invoke();
        takeHitParticle.Play();
    }

    private void CollisionTimer()
    {
        _collider.enabled = false;
        DOVirtual.DelayedCall(0.1f, () => _collider.enabled = true);
    }

    protected override void Die()
    {
        base.Die();
        DeadAction?.Invoke();
        CoinManager.Instance.GainCoin(1);
        PoolManager.Instance.Despawn(Pools.Types.Enemy, gameObject);
    }

    private void ShakeBody()
    {
        transform.GetChild(0).DOShakeRotation(0.2f).OnComplete(() => transform.GetChild(0).localEulerAngles = Vector3.zero);
    }

    public void ParticleColor(Material mat)
    {
        var renderer = takeHitParticle.GetComponent<Renderer>();
        renderer.material.color = mat.color;
        renderer.material.SetColor("_EmissionColor", mat.GetColor("_EmissionColor"));
    }

}