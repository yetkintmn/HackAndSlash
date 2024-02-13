using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMN.PoolManager;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemyScriptable> enemyScriptableList;

    [SerializeField] private float spawnDistance = 20f;

    private readonly List<Enemy> _enemyList = new();

    private void Start()
    {
        DOVirtual.DelayedCall(1, () => StartCoroutine(NewWave()));
    }

    private IEnumerator NewWave()
    {
        var wave = WaveManager.Instance.IncreaseWave();
        var amount = wave * 2 + 20;
        for (var i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var newEnemyObj = PoolManager.Instance.Spawn(Pools.Types.Enemy, transform);
        newEnemyObj.transform.localPosition = FindRandomSpawnPoint();
        var newEnemy = newEnemyObj.GetComponent<Enemy>();
        newEnemy.SetScriptable(FindRandomEnemyScriptable());
        newEnemy.EnemySpawnerDeadAction = EnemyDead;
        _enemyList.Add(newEnemy);
    }

    private Vector3 FindRandomSpawnPoint()
    {
        var playerPos = Player.Instance.transform.position;
        var randX = Random.Range(-spawnDistance, spawnDistance);
        var randZ = playerPos.z - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(playerPos.x - randX, 2) - spawnDistance * spawnDistance));
        if (Random.Range(-1, 1) < 0)
            randZ *= -1;
        var randPos = Vector3.right * randX + Vector3.forward * randZ;
        return randPos;
    }

    private EnemyScriptable FindRandomEnemyScriptable()
    {
        var wave = WaveManager.Instance.Wave;
        if (wave < 4)
            return enemyScriptableList[Random.Range(0, 2)];
        else if (wave < 10)
            return enemyScriptableList[Random.Range(2, 4)];
        else
            return enemyScriptableList[Random.Range(2, enemyScriptableList.Count)];
    }

    public void EnemyDead(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        if (_enemyList.Count <= 0)
            StartCoroutine(NewWave());
    }
}
