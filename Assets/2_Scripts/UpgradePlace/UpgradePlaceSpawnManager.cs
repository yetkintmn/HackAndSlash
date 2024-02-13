using System.Collections;
using System.Collections.Generic;
using TMN.PoolManager;
using UnityEngine;

public class UpgradePlaceSpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnDistance;
    [SerializeField] private float spawnTime;

    [SerializeField] private List<UpgradeScriptable> upgradeScriptableList;
 
    private void Start()
    {
        StartCoroutine(SpawnUpgradePlaceBegin());
    }
    
    private IEnumerator SpawnUpgradePlaceBegin()
    {
        yield return new WaitForSeconds(120);
        do
        {
            var randNum = Random.Range(1, 3);
            for (var i = 0; i < randNum; i++)
                SpawnUpgradePlace();
            yield return new WaitForSeconds(spawnTime);
        } while (true);
    }

    [ContextMenu("Spawn Upgrade Place")]
    private void SpawnUpgradePlace()
    {
        var newUpgradePlace = PoolManager.Instance.Spawn(Pools.Types.UpgradePlace).GetComponent<UpgradePlace>();
        newUpgradePlace.transform.position = FindRandomSpawnPoint();
        newUpgradePlace.SetScriptable(upgradeScriptableList[Random.Range(0, upgradeScriptableList.Count)]);        
    }

    private Vector3 FindRandomSpawnPoint()
    {
        var playerPos = Player.Instance.transform.position;
        var randX = Random.Range(-spawnDistance, spawnDistance);
        var randZ = playerPos.z - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(playerPos.x - randX, 2) - spawnDistance * spawnDistance));
        if (Random.Range(-1, 1) < 0)
            randZ *= -1;
        var randPos = Vector3.right * randX + Vector3.up * 0.1f + Vector3.forward * randZ;
        return randPos;
    }
}
