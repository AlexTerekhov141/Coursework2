using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    private PlayerController _playerController;
    private UnityEngine.Object Explosion;

    public GameObject dropObject;

    // Start is called before the first frame update
    float speed = 2f;
    public float fireRate = 5f; // ����� ����� ����������
    private float nextFireTime = 0f;
    Transform Target;
    Rigidbody2D rigidbody2d;
    Animator animator;

    public float Health { get; set; }
    
    [SerializeField]public float MaxHealt = 2f;
    
    bool aggressive = true;
    public GameObject projectilePrefab;
    public InputAction Projectile;
    //blink 
    private Material matblink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    void Start()
    {
        Debug.Log("Health" + Health);
        spriteRend = GetComponent<SpriteRenderer>();
        matblink = Resources.Load("PlayerBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
        _playerController = FindObjectOfType<PlayerController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Explosion = Resources.Load("WFX_ExplosiveSmoke");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Health = MaxHealt;
        MaxHealt = Health;
        if (!aggressive)
        {
            return;
        }
        Vector3 direction = Target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distance = direction.magnitude;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        
        
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        if (_playerController.time >= nextFireTime)
        {
            Launch();
            nextFireTime = _playerController.time + fireRate;
        }

    }
    public void Fix()
    {
        aggressive = false;
        rigidbody2d.simulated = false;
    }
    public void DestroyEnemy()
    {
        spriteRend.material = matblink;
        Debug.Log("enemy" + MaxHealt);
        MaxHealt = MaxHealt - _playerController.DamageTo;
        if (MaxHealt <= 0)
        {
            ExplosionFunc();
            Instantiate(dropObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }else
        {
            Invoke("ResetMaterial", .1f);
        }
        
    }
    public void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }
    void Launch()
    {
        
        Vector2 direction = transform.up;
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
        EnemyMissileScript projectile = projectileObject.GetComponent<EnemyMissileScript>();
        projectile.Launch(direction, 600);
    }
    public void ExplosionFunc()
    {
        GameObject explosionRef = (GameObject)Instantiate(Explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        Instantiate(dropObject, transform.position, Quaternion.identity);
        Destroy(explosionRef, 2f);
    }
    
    public void SetHealth(float newHealth)
    {
        MaxHealt += newHealth;
    }

}
