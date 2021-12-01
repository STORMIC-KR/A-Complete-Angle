using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Basic")]
    [SerializeField] private Player player;
    [SerializeField] private Transform targetPlayer;
    SpriteRenderer sr;
    public GameObject deathEffect;
    public ObjectPool objectPool;
    Rigidbody2D rb;
    Vector2 movement;
    public int enemyHealth;
    int maxEnemyHealth;
    public float speed;
    public float attackRange;

    [Header("Shot")]
    public GameObject bullet;
    public AudioSource shotSound;
    public Transform shotPoint;
    public float timeBtwShots;
    float shotTime;

    [Header("ItemDrop")]
    public GameObject healPack;
    public GameObject speedUp;

    void Start()
    {
        player = FindObjectOfType<Player>();
        targetPlayer = GameObject.Find("Player").transform;
        shotSound = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();

        objectPool = FindObjectOfType<ObjectPool>();

        rb = this.GetComponent<Rigidbody2D>();
        maxEnemyHealth = enemyHealth;
    }

    void Update()
    {
        Vector3 direction = targetPlayer.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        
        SearchAndShot();
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void SearchAndShot()
    {
        Vector2 direction = targetPlayer.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(Vector3.Distance(targetPlayer.position, transform.position) <= attackRange)
        {
            if (Time.time >= shotTime)
            {
                shotSound.Play();
                GameObject bulletObj = objectPool.MakeObject("EnemyBullet");
                bulletObj.transform.position = shotPoint.position;
                bulletObj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                shotTime = Time.time + timeBtwShots;
            }
        }
        if(Vector3.Distance(targetPlayer.position, transform.position) >= attackRange * 7)
        {
            gameObject.SetActive(false);
        }
    }

    void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        StartCoroutine("alphaBlink");
        if(enemyHealth <= 0)
        {
            int ran = Random.Range(0,10);

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);

            if(ran < 5)
            {
                Debug.Log("No Item");
            }
            else if(ran < 7)
            {
                GameObject healPack = objectPool.MakeObject("HealPack");
                healPack.transform.position = transform.position;
            }
            else if(ran < 8)
            {
                GameObject speedUp = objectPool.MakeObject("SpeedUp");
                speedUp.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            player.killEnemyCount++;
            Debug.Log("Enemy Down!");
            Invoke("Restore", 0.1f);
        }
    }

    void Restore()
    {
        enemyHealth = maxEnemyHealth;
    }

    IEnumerator alphaBlink()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(player.damage);
        }
    }
}
