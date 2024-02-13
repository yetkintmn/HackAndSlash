using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyAI EnemyAI { get; private set; }
    public EnemyAction EnemyAction { get; private set; }
    public EnemyAttribute EnemyAttribute { get; private set; }

    private MeshRenderer _meshRenderer;

    [field: SerializeField] public EnemyScriptable EnemyScriptable { get; private set; }

    public UnityAction<Enemy> EnemySpawnerDeadAction;

    private void Awake()
    {
        EnemyAI = GetComponent<EnemyAI>();
        EnemyAction = GetComponent<EnemyAction>();
        EnemyAttribute = GetComponent<EnemyAttribute>();
        _meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        EnemyAttribute.TakeHitAction += TakeHit;
        EnemyAttribute.DeadAction += Dead;
    }

    public void SetScriptable(EnemyScriptable enemyScriptable)
    {
        EnemyScriptable = enemyScriptable;
        EnemyAttribute.SetMaxHealth(enemyScriptable.maxHealth);
        EnemyAttribute.ParticleColor(enemyScriptable.material);
        EnemyAction.damage = enemyScriptable.damage;
        EnemyAI.SetSpeed(enemyScriptable.speed);
        transform.GetChild(0).localEulerAngles = Vector3.zero;
        transform.GetChild(0).localScale = Vector3.one * enemyScriptable.size;
        var collider = transform.GetComponent<BoxCollider>();
        collider.size = new Vector3(enemyScriptable.size, collider.size.y, enemyScriptable.size);
        _meshRenderer.material = enemyScriptable.material;
    }

    public void TakeHit()
    {
        EnemyAI.TakeHit();
    }

    public void Dead()
    {
        EnemySpawnerDeadAction?.Invoke(this);
        EnemyAI.Dead();
    }
}