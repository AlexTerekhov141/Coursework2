using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    Rigidbody2D rigidbody2d;


    // Awake is called when the Projectile GameObject is instantiated
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > 10000.0f)
        {
            Destroy(gameObject);
        }
    }


    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = gameObject.GetComponent<PlayerController>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        NewEnemy enemy2 = other.gameObject.GetComponent<NewEnemy>();
        Boss enemy3 = other.gameObject.GetComponent<Boss>();

        if (enemy != null)
        {
            enemy.DestroyEnemy();
            Destroy(gameObject);
        }

        if (enemy2 != null)
        {
            enemy2.DestroyEnemy();
            Destroy(gameObject);
        }

        if (enemy3 != null)
        {
            enemy3.DestroyEnemy();
            Destroy(gameObject);
        }
    }
}
