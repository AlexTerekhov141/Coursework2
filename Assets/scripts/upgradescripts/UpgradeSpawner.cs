using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject UpgradePrefab;
    public Transform playerTransform;
    public float minDistanceFromPlayer = 5f;
    public float maxDistanceFromPlayer = 10f;
    public float timeBeforeUpgrade = 5f;
    private PlayerController _playerController;
    float spawnInterval = 30f;
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(SpawnUpgrade());
        
    }

    IEnumerator SpawnUpgrade()
    {
        while (true)
        {
            if (_playerController.time >= timeBeforeUpgrade)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer)
                {
                    spawnPosition = GetRandomSpawnPosition();
                }

                Instantiate(UpgradePrefab, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
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
