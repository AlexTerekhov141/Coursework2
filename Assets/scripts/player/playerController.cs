using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    public GameObject shieldPrefab;
    private ScoreManager _scoreManager;
    private gun[] _gun;
    public float shieldDamage = 1f;
    public static bool GameIsPaused = false;
    public bool isShieldAdded { get; private set; }
    public GameObject pauseMenuUI;
    public GameObject endMenuUi;
    public GameObject HudMenuUi;
    public float Health { get; private set; }
    public float LvlToShow {  get; private set; }  
    public float ExpToShow { get; private set; }
    public float ExpToNeed { get; private set; }
    public float DamageTo { get; private set; }
    
    public float ShieldTo { get; private set; }
    public int GunTo { get; set; }
    private int gunStyle = 0;
    public float points;
    public float PointsToShow { get; private set; }
    public float damage = 1f;
    public float needExp = 10;
    public float MaxHealth = 3f;
    public float Exp = 0;
    public float Lvl = 0;
    /////variables for move
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 10.0f;
    /// variables for projectile
    public GameObject projectilePrefab;
    public InputAction Projectile;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    public float fireRate = 1f; 
    private float nextFireTime = 0f; 
    //blink 
    private Material matblink;
    private Material matDefault;
    private SpriteRenderer spriteRend;

    public float time = 0f;
    // Use this for initialization
    void Start()
    {
        isShieldAdded = false;
        _scoreManager = GetComponent<ScoreManager>();
        _gun = FindObjectsOfType<gun>();
        spriteRend = GetComponent<SpriteRenderer>();
        matblink = Resources.Load("PlayerBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gunStyle = GunTo;
        time = Time.timeSinceLevelLoad;
        PointsToShow = points;
        ShieldTo = shieldDamage;
        DamageTo = damage;
        LvlToShow = Lvl;
        ExpToShow = Exp;
        Health = MaxHealth;
        ExpToNeed = needExp;
        ////mouse control
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //////
        /////move
        Vector2 move = MoveAction.ReadValue<Vector2>();
        
        Vector2 position = (Vector2)transform.position + move * (speed * Time.deltaTime); ;
        transform.position = position;
        
        
        if (time >= nextFireTime)
        {
            
            Launch();
            nextFireTime = time + fireRate;
        }
    }
    void Launch()
    {
        if (gunStyle == 0)
        {
            _gun[1].LaunchPlayer();
        }else if (gunStyle == 2)
        {
            _gun[0].LaunchPlayer();
            _gun[1].LaunchPlayer();
            _gun[2].LaunchPlayer();
        }else if (gunStyle == 1)
        {
            _gun[0].LaunchPlayer();
            _gun[2].LaunchPlayer();
        }
    }
    public void DestroyPlayer(float Damage)
    {
        
        Debug.Log(MaxHealth);
        MaxHealth = MaxHealth - Damage;
        spriteRend.material = matblink;
        if (MaxHealth <= 0)
        {
            HudMenuUi.SetActive(false);
            Debug.Log("End");
            endMenuUi.SetActive(true);
            Time.timeScale = 0f;
        }else
        {
            Invoke("ResetMaterial", .1f);
        }
    }

    
    public void changeExp(float exp)
    {
        Exp += exp;
        Debug.Log("Current Exp: " + Exp);
        points += 10;
        if (Exp >= needExp)
        {
            Exp = Exp - needExp;
            Lvl++;
            needExp *= 2;
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            GameIsPaused = true;
            Debug.Log("Level Up! New Level: " + Lvl);
            Debug.Log("New Required Exp: " + needExp);
        }
    }
    public void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }
    public void upgradeDamege()
    {
        damage = damage + 1f;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void updgradeSpeed()
    {
        speed = speed + 5f;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void upgradeFireRate()
    {
        if (fireRate != 0.1f)
        {
            fireRate = fireRate - 0.1f;
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void addShield()
    {
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        isShieldAdded = true;
        shield.transform.parent = transform; 
    }

    public void upgradeShield()
    {
        shieldDamage = shieldDamage + 1f;
    }
    public void exit()
    {
        Time.timeScale = 1f;
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject o in objects)
            {
                Destroy(o);
            }
            Destroy(gameObject);
        SceneManager.LoadScene(2);
    }
}