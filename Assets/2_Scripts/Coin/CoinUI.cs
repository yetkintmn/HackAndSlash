using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        CoinManager.Instance.CoinTextAction = SetCoinText;
        SetCoinText(CoinManager.Instance.Coin);
    }

    public void SetCoinText(int value)
    {
        coinText.text = value.ToString();
    }
}