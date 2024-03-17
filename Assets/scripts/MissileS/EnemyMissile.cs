using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour
{
    Rigidbody2D _rigidbody2d;
    public float DamageTo { get; private set; }
    public float Damage = 1f;

    // Awake is called when the Projectile GameObject is instantiated
    void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DamageTo = Damage;
        if (transform.position.magnitude > 10000.0f)
        {
            Destroy(gameObject);
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
}
