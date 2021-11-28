using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float normalAcceleration;
    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;



    public int cur_playerHealth = 10;
    public int max_playerHealth = 10;

    public int damage;

    public int killEnemyCount = 0;
    public Text killCountText;


    public Transform wing;

    public Slider healthBar;
    public GameObject gameOverPanel;
    public Animator mapAnimator;
    public GameObject deathEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = normalAcceleration;

        healthBar.value = (float)cur_playerHealth / (float)max_playerHealth;
        sr = GetComponent<SpriteRenderer>();

        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(directionX, directionY).normalized;



        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        wing.up = (mousePos - (Vector2)transform.position).normalized;

        healthBar.value = (float)cur_playerHealth / (float)max_playerHealth;

        killCountText.text = "Kill \n" + killEnemyCount;

        WeaponSwap();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movementInput.x * acceleration, movementInput.y * acceleration);
    }

    public void WeaponSwap()
    {
        if (Input.GetMouseButton(1))
        {
            wing.GetChild(0).gameObject.SetActive(false);
            wing.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            wing.GetChild(0).gameObject.SetActive(true);
            wing.GetChild(1).gameObject.SetActive(false);
        }
    }

    void TakeDamage(int damage)
    {
        cur_playerHealth -= damage;
        StartCoroutine("AlphaBlink");

        if(cur_playerHealth <= 0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.2f);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }

    IEnumerator AlphaBlink()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }

    // void ZoomMinimap()
    // {
    //     if(Input.GetKey(KeyCode.C))
    //     {
    //         mapAnimator.SetFloat("speed", 1);
    //         mapAnimator.Play("MinimapZoom");
    //     }
    //     if(Input.GetKeyUp(KeyCode.C))
    //     {
    //         mapAnimator.SetFloat("speed", -1);
    //         mapAnimator.Play("MinimapZoom");
    //     }
    // }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy_Bullet"))
        {
            TakeDamage(1);
        }
    }
}