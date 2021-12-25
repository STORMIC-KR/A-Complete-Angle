using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Basic Movement")]
    [SerializeField] private Player player;
    public enum Type { Normal, Giant };
    public Type enemyType;
    SpriteRenderer sr;
    public GameObject deathEffect;
    public ObjectPool objectPool;
    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;

    [Header("Enemy Stats")]
    public int enemyHealth;
    int maxEnemyHealth;
    public float speed;
    public float attackRange;
    Enemy enemyScript;

    [Header("Shot")]
    public AudioSource shotSound;
    [SerializeField] private Transform targetPlayer;
    public Transform shotPoint;
    public float timeBtwShots;
    float shotTime;

    [Header("ItemDrop")]
    public GameObject healPack;
    public GameObject speedUp;

    void Start()
    {
        maxEnemyHealth = enemyHealth;
        player = FindObjectOfType<Player>();
        targetPlayer = GameObject.Find("Player").transform;
        shotSound = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyScript = GetComponent<Enemy>();

        objectPool = FindObjectOfType<ObjectPool>();

        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = targetPlayer.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        if(player.cur_playerHealth <= 0)
        {
            enemyScript.enabled = false;
        }
        else if(player.cur_playerHealth > 0)
        {
            enemyScript.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);

        SearchAndShot();
        Dead();
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
                anim.Play("Enemy_Attack");
                shotSound.Play();
                GameObject bulletObj;
                switch(enemyType)
                {
                    case Type.Normal:
                        bulletObj = objectPool.MakeObject("N_EnemyBullet");
                        bulletObj.transform.position = shotPoint.position;
                        bulletObj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                        shotTime = Time.time + timeBtwShots;
                        break;
                    case Type.Giant:
                        bulletObj = objectPool.MakeObject("G_EnemyBullet");
                        bulletObj.transform.position = shotPoint.position;
                        bulletObj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                        shotTime = Time.time + timeBtwShots;
                        break;
                }
            }
        }
    }

    void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        StartCoroutine("alphaBlink");
    }

    void Dead()
    {
        if(enemyHealth <= 0)
        {
            gameObject.SetActive(false);
            
            int itemRandomNum = Random.Range(0,10);
            player.killEnemyCount++;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Invoke("Restore", 0.1f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);

            if(itemRandomNum < 4)
            {
                return;
            }
            else if(itemRandomNum < 8)
            {
                GameObject healPack = objectPool.MakeObject("HealPack");
                healPack.transform.position = transform.position;
            }
            else if(itemRandomNum < 9)
            {
                GameObject speedUp = objectPool.MakeObject("ShootUp");
                speedUp.transform.position = transform.position;
            }
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
            int damage = other.gameObject.GetComponent<Bullet>().damage;
            TakeDamage(damage);
        }
    }
}
