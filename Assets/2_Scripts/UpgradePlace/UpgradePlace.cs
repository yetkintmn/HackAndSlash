using System.Collections;
using TMN.PoolManager;
using TMPro;
using UnityEngine;

public class UpgradePlace : MonoBehaviour
{
    [SerializeField] private SpriteRenderer upgradeIcon;
    [SerializeField] private TextMeshPro priceText;

    private UpgradeType _upgradeType;
    private int _price = 10;

    private IEnumerator _spendingCoroutine;

    public void SetScriptable(UpgradeScriptable upgradeScriptable)
    {
        upgradeIcon.sprite = upgradeScriptable.icon;
        _upgradeType = upgradeScriptable.upgradeType;
        _price = upgradeScriptable.price;
        SetPriceText();      
    }

    public void SpendCoinBegin()
    {
        if (_spendingCoroutine != null)
            StopCoroutine(_spendingCoroutine);
        _spendingCoroutine = Spending();
        StartCoroutine(_spendingCoroutine);
    }

    public void SpendCoinStop()
    {
        if (_spendingCoroutine != null)
            StopCoroutine(_spendingCoroutine);
    }

    private IEnumerator Spending()
    {
        while (CoinManager.Instance.Coin > 0 && _price > 0)
        {
            yield return new WaitForSeconds(0.05f);
            CoinManager.Instance.SpendCoin(1);
            ReducePrice(1);
        }

        if (_price <= 0)
        {
            Upgrade();
            PoolManager.Instance.Despawn(Pools.Types.UpgradePlace, gameObject);
        }
    }

    private void ReducePrice(int amount)
    {
        _price -= amount;
        SetPriceText();
    }

    private void SetPriceText()
    {
        priceText.text = _price.ToString();
    }

    private void Upgrade()
    {
        switch (_upgradeType)
        {
            case UpgradeType.AddWeapon:
                Player.Instance.PlayerWeapon.AddNewWeapon();
                break;
            case UpgradeType.LevelUpWeapon:
                Player.Instance.PlayerWeapon.UpgradeRandomWeapon();
                break;
            case UpgradeType.Heal:
                Player.Instance.PlayerAttribute.Heal(10);
                break;
        }
    }
}
