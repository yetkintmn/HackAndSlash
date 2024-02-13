using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Create New Weapon")]
public class WeaponScriptable : ScriptableObject
{
    public string weaponName;

    public int damage;
    public float turnSpeed;
}