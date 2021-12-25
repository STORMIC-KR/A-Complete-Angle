using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [Header("Basic Movement")]
    public float normalAcceleration;
    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;
    Rigidbody2D rb;
    SpriteRenderer sr;

    [Header("Player Stats")]
    public int cur_playerHealth;
    public int max_playerHealth;

    public int killEnemyCount = 0;
    public Text killCountText;

    Player playerScript;

    [Header("Player Gadgets")]
    public Transform wing;
    public GameObject attackWing;
    public GameObject deathEffect;
    public GameObject crossHair;

    public Slider healthBar;
    public Animator mapAnimator;
    public GameManager gameManager;

    public AudioSource shootItemSound;
    public AudioSource healPackSound;
    AttackWeapon attackWeaponScript;
    WaveSpawnSystem waveScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = normalAcceleration;

        healthBar.value = (float)cur_playerHealth / (float)max_playerHealth;
        sr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();

        playerScript = GetComponent<Player>();
        attackWeaponScript = FindObjectOfType<AttackWeapon>().GetComponent<AttackWeapon>();
        waveScript = FindObjectOfType<WaveSpawnSystem>().GetComponent<WaveSpawnSystem>();
    }

    void Update()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");
            movementInput = new Vector2(directionX, directionY).normalized;
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wing.up = (mousePos - (Vector2)transform.position).normalized;
        }

        healthBar.value = (float)cur_playerHealth / (float)max_playerHealth;

        killCountText.text = "Kill \n" + killEnemyCount;

        if(cur_playerHealth <= 0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            StartCoroutine(GameOver());
        }
        else if(cur_playerHealth > 0)
        {
            playerScript.enabled = true;
            attackWeaponScript.enabled = true;
            waveScript.enabled = true;
        }

        WeaponSwap();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movementInput.x * acceleration, movementInput.y * acceleration);
    }

    public void WeaponSwap()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (Input.GetMouseButton(1))
            {
                wing.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                wing.GetChild(0).gameObject.GetComponent<AttackWeapon>().enabled = false;

                wing.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                wing.GetChild(1).gameObject.GetComponent<DefenseWeapon>().enabled = true;
                wing.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                wing.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                wing.GetChild(0).gameObject.GetComponent<AttackWeapon>().enabled = true;

                wing.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                wing.GetChild(1).gameObject.GetComponent<DefenseWeapon>().enabled = false;
                wing.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    void TakeDamage(int damage)
    {
        cur_playerHealth -= damage;
        StartCoroutine(AlphaBlink());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.2f);
        Cursor.visible = true;
        crossHair.SetActive(false);
        playerScript.enabled = false;
        attackWeaponScript.enabled = false;
        waveScript.enabled = false;

        //Time.timeScale = 0;
    }

    IEnumerator AlphaBlink()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy_Bullet"))
        {
            int damage = other.gameObject.GetComponent<Bullet>().damage;
            TakeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("ShootUp"))
        {
            shootItemSound.Play();
        }
        else if(other.CompareTag("HealPack"))
        {
            if(cur_playerHealth >= max_playerHealth)
            {
                return;
            }
            else
            {
                healPackSound.Play();
            }
        }
    }
}