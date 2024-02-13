using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Create New Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public string enemyName;
    public int maxHealth = 2;
    public int damage = 1;
    public float speed = 1f;
    public float size = 1f;
    public Material material;
}
