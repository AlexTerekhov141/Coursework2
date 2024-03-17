using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        NewEnemy enemy2 = other.gameObject.GetComponent<NewEnemy>();
        Boss enemy3 = other.gameObject.GetComponent<Boss>();

        if (enemy != null)
        {
            enemy.DestroyEnemy();
            
        }

        if (enemy2 != null)
        {
            enemy2.DestroyEnemy();
            
        }

        if (enemy3 != null)
        {
            enemy3.DestroyEnemy();
            
        }
    }
}
