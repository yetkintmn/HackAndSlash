using DG.Tweening;
using TMNLib;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        MoveToTarget = 1,
        Attack = 2,
        TakeHit = 3,
        Dead = 4,
    }

    [SerializeField] private State state;

    private Transform _target;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        state = State.Idle;
    }

    private void Start()
    {
        _target = Player.Instance.transform;
    }

    void Update()
    {
        AIState();
    }

    private void AIState()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.MoveToTarget:
                MoveToTarget();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        StopMove();
        state = State.MoveToTarget;
    }

    private void MoveToTarget()
    {
        if (_target == null)
        {
            state = State.Idle;
            return;
        }

        if (IsReachedTarget(0))
        {
            state = State.Attack;
            return;
        }

        if (_agent.enabled && _agent.isOnNavMesh)
        {
            _agent.isStopped = false;
            var velocity = _agent.velocity.magnitude / _agent.speed;
            _agent.SetDestination(_target.position);
        }
    }

    private bool IsReachedTarget(float minDis = 0.5f)
    {
        if (_target != null)
            return EasyMethods.IsEqualV3(transform.position, _target.position, minDis);
        return false;
    }

    private void Attack()
    {
        LookAtTarget();
        StopMove();
        //transform.DOJump(transform.forward, 2, 1, 0.3f).OnComplete(() => state = State.MoveToTarget);
    }

    private void StopMove()
    {
        if (!_agent.enabled || !_agent.isOnNavMesh || _agent.isStopped)
            return;
        _agent.isStopped = true;
    }

    private void LookAtTarget()
    {
        transform.LookAt(_target);
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }

    public void TakeHit()
    {
        StopMove();
        state = State.TakeHit;
        transform.DOJump(transform.localPosition + transform.forward * 12, Random.Range(2f, 3f), 1, 0.6f).OnComplete(() => state = State.Idle);
    }

    public void Dead()
    {
        state = State.Dead;
    }
}