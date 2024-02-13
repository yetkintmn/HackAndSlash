using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerAttribute>(out var playerAttribute))
        {
            playerAttribute.TakeHit(damage);
        }
    }
}