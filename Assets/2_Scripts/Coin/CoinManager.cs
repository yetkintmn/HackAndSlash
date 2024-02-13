using System;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoSingleton<CoinManager>
{
    [field: SerializeField] public int Coin { get; private set; }

    public UnityAction<int> CoinTextAction;

    public void GainCoin(int amount)
    {
        Coin += amount;
        CoinTextAction?.Invoke(Coin);
    }

    public void SpendCoin(int amount)
    {
        Coin -= amount;
        CoinTextAction?.Invoke(Coin);
    }

    [ContextMenu("Add 500 Coin")]
    private void Add500Coin()
    {
        Coin += 500;
        CoinTextAction?.Invoke(Coin);
    }
}