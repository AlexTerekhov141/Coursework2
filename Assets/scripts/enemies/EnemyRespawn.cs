using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject newEnemyPrefab;
    public GameObject newBossPrefab;
    public Transform playerTransform;
    private Enemy _enemy;
    public float minDistanceFromPlayer = 5f;
    public float maxDistanceFromPlayer = 10f;
    public float timeBeforeNewEnemies = 5f;
    public float timeBeforeNewBoss = 10f;
    private PlayerController _playerController;
    float spawnInterval = 3f;
    void Start()
    {
        _enemy = FindObjectOfType<Enemy>();
        _playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnNewEnemies());
        StartCoroutine(SpawnNewBoss());
    }

   
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer)
            {
                spawnPosition = GetRandomSpawnPosition();
            }
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnNewEnemies()
    {
        
            while (true)
            {
                if (_playerController.time >= timeBeforeNewEnemies)
                {
                    Vector3 spawnPosition = GetRandomSpawnPosition();
                    while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer)
                    {
                        spawnPosition = GetRandomSpawnPosition();
                    }

                    Instantiate(newEnemyPrefab, spawnPosition, Quaternion.identity);
                    yield return new WaitForSeconds(spawnInterval);
                }
                else
                {
                    yield return null;
                }
            }
        
            
        
    }
    IEnumerator SpawnNewBoss()
    {
        
        while (true)
        {
            if (_playerController.time >= timeBeforeNewBoss)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer)
                {
                    spawnPosition = GetRandomSpawnPosition();
                }

                Instantiate(newBossPrefab, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(timeBeforeNewBoss);
            }
            else
            {
                yield return null;
            }
        }
        
            
        
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomDistance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        float randomAngle = Random.Range(0f, 360f);
        Vector3 spawnOffset = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0) * randomDistance;
        return playerTransform.position + spawnOffset;
    }
}
