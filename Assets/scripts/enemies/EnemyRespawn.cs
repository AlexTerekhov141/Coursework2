using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject TextUi;
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
    private float UpgradeTime = 20f;
    [SerializeField] public float upgradeDamage = 1f;
    float spawnInterval = 3f;
    private float upradeHealthEnemy = 0f;
    private float upradeHealthNewEnemy = 0f;
    private float upradeHealthBoss = 0f;
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
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
            enemyComponent.SetHealth(upradeHealthEnemy);
            if (_playerController.time > UpgradeTime)
            {
                upgradeDamage += 1f;
                upradeHealthEnemy += 3f;
                StartCoroutine(ShowTextUiForDuration(2f));
                UpgradeTime = UpgradeTime + _playerController.time;
            }

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

                    GameObject newEnemy = Instantiate(newEnemyPrefab, spawnPosition, Quaternion.identity);
                    NewEnemy enemyComponent = newEnemy.GetComponent<NewEnemy>();
                    enemyComponent.SetHealth(upradeHealthNewEnemy);
                    if (_playerController.time > UpgradeTime)
                    {
                        upradeHealthNewEnemy += 2f;
                    }

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

                GameObject newEnemy = Instantiate(newBossPrefab, spawnPosition, Quaternion.identity);
                Boss enemyComponent = newEnemy.GetComponent<Boss>();
                enemyComponent.SetHealth(upradeHealthBoss);
                if (_playerController.time > UpgradeTime)
                {
                    upradeHealthBoss += 7f;
                }

                yield return new WaitForSeconds(timeBeforeNewBoss);
            }
            else
            {
                yield return null;
            }
        }
        
            
        
    }
    private IEnumerator ShowTextUiForDuration(float duration)
    {
        TextUi.SetActive(true);
        yield return new WaitForSeconds(duration);
        TextUi.SetActive(false);
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomDistance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        float randomAngle = Random.Range(0f, 360f);
        Vector3 spawnOffset = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0) * randomDistance;
        return playerTransform.position + spawnOffset;
    }
}
