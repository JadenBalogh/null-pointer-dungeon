using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float minSpawnInterval = 5f;
    [SerializeField] private float maxSpawnInterval = 10f;
    [SerializeField] private int maxSpawnCount = 3;
    [SerializeField] private Enemy[] enemyPrefabs;

    private int spawnCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (spawnCount < maxSpawnCount)
            {
                int enemyIdx = Random.Range(0, enemyPrefabs.Length);
                Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * 0.2f;
                Enemy enemy = Instantiate(enemyPrefabs[enemyIdx], spawnPos, Quaternion.identity);
                enemy.OnDeath.AddListener(DecreaseSpawnCount);
                spawnCount++;
            }

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void DecreaseSpawnCount()
    {
        spawnCount--;
    }
}
