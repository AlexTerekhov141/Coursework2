using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = gameObject.GetComponent<PlayerController>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        NewEnemy enemy2 = other.gameObject.GetComponent<NewEnemy>();
        Boss enemy3 = other.gameObject.GetComponent<Boss>();

        if (enemy != null)
        {
            enemy.DestroyEnemyByShield();
        }

        if (enemy2 != null)
        {
            enemy2.DestroyEnemyByShield();
            
        }

        if (enemy3 != null)
        {
            enemy3.DestroyEnemyByShield();
            
        }
    }
}
