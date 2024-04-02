using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject dropObject;
    private gun _gun;
    private gun1 _gun1;
    private gun2 _gun2;
    private UnityEngine.Object Explosion;
    public GameObject projectilePrefab;
    // Start is called before the first frame update
    float speed = 1f;
    public float fireRate = 5f; // ����� ����� ����������
    private float nextFireTime = 0f;
    Transform Target;
    Rigidbody2D rigidbody2d;
    Animator animator;
    
    public float Health { get; set; }
    public float MaxHealt = 10f;
    bool aggressive = true;
    float upgradeInterval = 40f;
    public InputAction Projectile;
    //blink 
    private Material matblink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    void Start()
    {
        _gun = FindObjectOfType<gun>();
        _gun1 = FindObjectOfType<gun1>();
        _gun2 = FindObjectOfType<gun2>();
        spriteRend = GetComponent<SpriteRenderer>();
        matblink = Resources.Load("PlayerBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
        playerController = FindObjectOfType<PlayerController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Explosion = Resources.Load("WFX_ExplosiveSmoke Big Alt");
    }
    // Update is called once per frame
    private void Update()
    {
        Health = MaxHealt;
        if (Time.time >= upgradeInterval)
        {
            MaxHealt += 10f;
            upgradeInterval = Time.time + upgradeInterval;
        }
    }
    void FixedUpdate()
    {
        if (!aggressive)
        {
            return;
        }
        Vector3 direction = Target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distance = direction.magnitude;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);


        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        if (Time.time >= nextFireTime)
        {

            Launch();
            nextFireTime = Time.time + fireRate;
            Debug.Log("Launchs");
        }

    }
    public void Fix()
    {
        aggressive = false;
        rigidbody2d.simulated = false;
    }
    public void DestroyEnemy()
    {

        MaxHealt = MaxHealt - playerController.DamageTo;
        spriteRend.material = matblink;
        if (MaxHealt <= 0)
        {
            GameObject explosionRef = (GameObject)Instantiate(Explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            Instantiate(dropObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosionRef, 2f);
        }
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }
    public void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }
    public void Launch()
    {
        _gun.Launch();
        _gun1.Launch();
        _gun2.Launch();
    }
    public void SetHealth(float newHealth)
    {
        MaxHealt += newHealth;
    }
}
