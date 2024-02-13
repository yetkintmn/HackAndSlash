using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Coin>(out var coin))
        {
            CoinManager.Instance.GainCoin(coin.Value);
        }
        else if (other.TryGetComponent<UpgradePlace>(out var upgradePlace))
        {
            upgradePlace.SpendCoinBegin();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<UpgradePlace>(out var upgradePlace))
        {
            upgradePlace.SpendCoinStop();
        }
    }
}
