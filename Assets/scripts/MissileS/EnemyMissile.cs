using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour
{
    Rigidbody2D _rigidbody2d;
    public PlayerController _playerController;
    public EnemySpawner _EnemySpawner;
    public float DamageTo { get; set; }
    public float Damage = 1f;
    private float timeUpgrade = 20f;
    
    void Awake()
    {
        _EnemySpawner = FindObjectOfType<EnemySpawner>();
        _playerController = FindObjectOfType<PlayerController>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DamageTo = Damage;
        if (transform.position.magnitude > 10000.0f)
        {
            Destroy(gameObject);
        }

        if (_playerController.time > timeUpgrade)
        {
            SetDamage(_EnemySpawner.upgradeDamage);
            Debug.Log("upgrade Weapon" + Damage);
            timeUpgrade = timeUpgrade + _playerController.time;
        }
    }


    public void Launch(Vector2 direction, float force)
    {
        _rigidbody2d.AddForce(direction * force);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
       
        if (player != null)
        {
            Destroy(gameObject);
            player.DestroyPlayer(Damage);
           
        }
    }
    public void SetDamage(float newDamage)
    {
        Damage += newDamage;
    }
}
