using System.Collections.Generic;
using TMN.PoolManager;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private List<WeaponScriptable> weaponScriptableList;

    [SerializeField] private List<Weapon> weaponList = new();

    private void Start()
    {
        AddNewWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddNewWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            UpgradeRandomWeapon();
        }
    }

    [ContextMenu("Add New Weapon")]
    public void AddNewWeapon()
    {
        if (weaponList.Count > 8)
            return;
        var newWeapon = PoolManager.Instance.Spawn(Pools.Types.Weapon, Player.Instance.transform).GetComponent<Weapon>();
        newWeapon.transform.localPosition = Vector3.up;
        if (weaponList.Count > 0)
            newWeapon.transform.localEulerAngles = weaponList[weaponList.Count - 1].transform.localEulerAngles + Vector3.up * 40;
        newWeapon.SetScriptable(weaponScriptableList[Random.Range(0, weaponScriptableList.Count)]);
        weaponList.Add(newWeapon);
    }

    public void UpgradeRandomWeapon()
    {
        var randWeapon = weaponList[Random.Range(0, weaponList.Count)];
        randWeapon.Upgrade();
    }
}