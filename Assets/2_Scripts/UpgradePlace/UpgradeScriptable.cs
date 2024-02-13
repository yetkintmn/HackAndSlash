using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "ScriptableObjects/Create New Upgrade")]
public class UpgradeScriptable : ScriptableObject
{
    public UpgradeType upgradeType;
    public int price;
    public Sprite icon;

    public WeaponScriptable weaponScriptable;
}

public enum UpgradeType
{
    AddWeapon = 0,
    LevelUpWeapon = 1,
    Heal = 2,
}