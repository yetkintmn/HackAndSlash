using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int _damage;
    private float _turnSpeed;

    private float _initialY;

    public void SetScriptable(WeaponScriptable weaponScriptable)
    {
        _damage = weaponScriptable.damage;
        _turnSpeed = weaponScriptable.turnSpeed;
        _initialY = transform.eulerAngles.y;
        StartRotate();
    }

    private void StartRotate()
    {
        transform.DORotate(Vector3.up * (90f + _initialY), 1f / _turnSpeed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyAttribute>(out var enemyAttribute))
            enemyAttribute.TakeHit(_damage);
    }

    public void Upgrade()
    {
        _damage += 2;
        _turnSpeed += 0.5f;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + 0.2f);
    }
}