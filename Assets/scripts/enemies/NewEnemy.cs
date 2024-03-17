using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewEnemy : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject dropObject;
    // Start is called before the first frame update
    float speed = 5f;
    private UnityEngine.Object Explosion;
    Transform Target;
    Rigidbody2D rigidbody2d;
    Animator animator;
    public float MaxHealt = 1f;
    bool aggressive = true;
    public float Health { get; set; }
    public float DamageTo { get; private set; }
    public float Damage = 5f;
    //blink 
    private Material matblink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        matblink = Resources.Load("PlayerBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
        playerController = FindObjectOfType<PlayerController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Explosion = Resources.Load("WFX_ExplosiveSmoke");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DamageTo = Damage;
        if (!aggressive)
        {
            return;
        }
        Vector3 direction = Target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distance = direction.magnitude;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);


        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
    }
    public void Fix()
    {
        aggressive = false;
        rigidbody2d.simulated = false;
    }
    public void DestroyEnemy()
    {
        spriteRend.material = matblink;
        MaxHealt = MaxHealt - playerController.DamageTo;
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

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();


        if (controller != null)
        {
            ExplosionFunc();
            controller.DestroyPlayer(Damage);
            Destroy(gameObject);
        }
    }
    public void ExplosionFunc()
    {
        GameObject explosionRef = (GameObject)Instantiate(Explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        Instantiate(dropObject, transform.position, Quaternion.identity);
        Destroy(explosionRef, 2f);
    }
}
