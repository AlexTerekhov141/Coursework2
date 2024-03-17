using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    // Start is called before the first frame update
    public void Launch()
    {
        Debug.Log("Launch gun");
        Vector2 direction = transform.up;
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
        EnemyMissileScript projectile = projectileObject.GetComponent<EnemyMissileScript>();
        projectile.Launch(direction, 600);
    }
    public void LaunchPlayer()
    {
        Debug.Log("Launch gun");
        Vector2 direction = transform.up;
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
        MissileScript projectile = projectileObject.GetComponent<MissileScript>();
        projectile.Launch(direction, 600);
    }
}
