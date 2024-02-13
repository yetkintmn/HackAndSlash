using UnityEngine;

[RequireComponent(typeof(PlayerAttribute))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWeapon))]
public class Player : MonoSingleton<Player>
{
    public PlayerAttribute PlayerAttribute { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerWeapon PlayerWeapon { get; private set; }

    private void Awake()
    {
        PlayerAttribute = GetComponent<PlayerAttribute>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerWeapon = GetComponent<PlayerWeapon>();
    }
}
