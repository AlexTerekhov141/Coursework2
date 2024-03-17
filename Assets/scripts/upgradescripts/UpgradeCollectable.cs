using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeCollectable : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private PlayerController _playerController;
    public float startTime = 0f;
    public float endTime =  2f;
    public GameObject pauseMenuUI;
    public GameObject GunUpgradeUi;
    public GameObject ShieldUpgradeUI;
    private bool isPlayerOnCollectable = false;
    private bool isWaiting = false;
    public Transform playerSpawnPoint;
    public float spawnRadius = 10f;
    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (_playerController.GunTo == 2)
        {
            GunUpgradeUi.SetActive(false);
            ShieldUpgradeUI.SetActive(true);
        }
        if (isPlayerOnCollectable && startTime >= endTime)
        {
            
            isPlayerOnCollectable = false;
            pause();
        }
        else if(isPlayerOnCollectable && !isWaiting)
        {
            StartCoroutine(WaitAndIncrease());
        }
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }
    IEnumerator WaitAndIncrease()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f);
        startTime++;
        isWaiting = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            isPlayerOnCollectable = true;
        }
    }

    
    public void upgradeWeapon()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        _playerController.GunTo += 1;
        Destroy(gameObject);
    }
    public void upgradeMaxHealth()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        _playerController.MaxHealth = _playerController.MaxHealth + 10f;
        Destroy(gameObject);   
    }

    public void addShieldToPlayer()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        _playerController.addShield();
        Destroy(gameObject);   
    }
}