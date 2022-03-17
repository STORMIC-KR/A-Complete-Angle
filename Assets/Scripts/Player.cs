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

    public float timeBtwShots;

    float maxLevelScore = 50f;
    public float levelScore = 0;
    public int levelCount = 1;

    [Header("Upgrade")]
    int speedUpgradeCount;
    int delayDownCount;
    int hpUpgradeCount;
    public int killEnemyCount = 0;
    Player playerScript;

    [Header("Player Gadgets")]
    public Transform wing;
    public GameObject attackWing;
    public GameObject deathEffect;
    public GameObject hitEffect;
    public GameObject crossHair;
    public GameObject upgradePanel;
    public GameObject gameOverPanel;
    public Slider healthBar;
    public Text upgradeHistoryText;
    public Text killCountText;
    public Text levelText;
    public Slider lvBar;

    public Button shotUpgradeBtn;

    public Text testTxt;

    public AudioSource shootItemSound;
    public AudioSource healItemSound;
    AttackWeapon attackWeaponScript;
    WaveSpawnSystem waveScript;
    AbilityHolder abilityHolder;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = normalAcceleration;

        healthBar.value = (float)cur_playerHealth / max_playerHealth;
        sr = GetComponent<SpriteRenderer>();

        playerScript = GetComponent<Player>();
        attackWeaponScript = FindObjectOfType<AttackWeapon>().GetComponent<AttackWeapon>();
        waveScript = FindObjectOfType<WaveSpawnSystem>().GetComponent<WaveSpawnSystem>();
        abilityHolder = FindObjectOfType<AbilityHolder>().GetComponent<AbilityHolder>();
    }

    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        movementInput = new Vector2(directionX, directionY).normalized;
        wing.up = (mousePos - (Vector2)transform.position).normalized;

        healthBar.value = (float)cur_playerHealth / (float)max_playerHealth;
        killCountText.text = "Kill \n" + killEnemyCount;

        if(cur_playerHealth <= 0)
        {
            cur_playerHealth = 0;
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

        upgradeHistoryText.text = "Stats" + "\nSpeed : " + normalAcceleration + "\nHP : " + max_playerHealth + "\nDelay : " + timeBtwShots;
        testTxt.text = "Speed " + normalAcceleration + "\nMax_HP " + max_playerHealth + "\nDelay " + timeBtwShots + "\nCur_HP " + cur_playerHealth;

        lvBar.value = levelScore / maxLevelScore;
        levelText.text = "Lv." + levelCount;

        WeaponSwap();
        LevelUP();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movementInput.x * acceleration, movementInput.y * acceleration);
    }

    public void WeaponSwap()
    {
        if (Input.GetMouseButton(1))
        {
            GameObject.Find("AttackWing").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("AttackWing").gameObject.GetComponent<AttackWeapon>().enabled = false;

            GameObject.Find("DefenseWing").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("DefenseWing").gameObject.GetComponent<DefenseWeapon>().enabled = true;
            GameObject.Find("DefenseWing").gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GameObject.Find("AttackWing").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("AttackWing").gameObject.GetComponent<AttackWeapon>().enabled = true;
                
            GameObject.Find("DefenseWing").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DefenseWing").gameObject.GetComponent<DefenseWeapon>().enabled = false;
            GameObject.Find("DefenseWing").gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        cur_playerHealth -= damage;
        StartCoroutine(AlphaBlink());
    }

    public void LevelScoreUP(float levelIncrease)
    {
        levelScore += levelIncrease;
    }

    public void LevelUP()
    {
        if(levelScore >= maxLevelScore)
        {
            upgradePanel.SetActive(true);
            Cursor.visible = true;
            crossHair.SetActive(false);
            levelScore = 0;
            levelCount++;
            Time.timeScale = 0;
        }
    }

    public void UpgradePlayer(string type)
    {
        //normalAcceleration = 5
        //max_playerHealth = 100
        //timeBtwShots = 0.5
        switch(type)
        {
            case "speed":
                normalAcceleration += 0.5f;
                speedUpgradeCount++;
            break;
            case "health":
                cur_playerHealth += 10;
                max_playerHealth += 10;
                hpUpgradeCount++;
            break;
            case "delay":
                timeBtwShots -= 0.01f;
                if(timeBtwShots <= 0.01)
                {
                    shotUpgradeBtn.interactable = false;
                }
                else
                {
                    shotUpgradeBtn.interactable = true;
                }
                delayDownCount++;
            break;
        }
        Time.timeScale = 1;
        Cursor.visible = false;
        crossHair.SetActive(true);
        upgradePanel.SetActive(false);
    }

    public void FinishGame()
    {
        gameOverPanel.SetActive(true);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.2f);
        Cursor.visible = true;
        Invoke("FinishGame", 0.3f);
        crossHair.SetActive(false);
        playerScript.enabled = false;
        attackWeaponScript.enabled = false;
        abilityHolder.enabled = false;
        waveScript.enabled = false;
        gameObject.SetActive(false);
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
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
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
                healItemSound.Play();
            }
        }
    }
}