using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Basic Movement")]
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    [SerializeField] private Player player;
    public enum Type { Normal, Giant, Explosive, Shield };
    public Type enemyType;
    public GameObject deathEffect;
    ObjectPool objectPool;

    [Header("Enemy Stats")]
    Enemy enemyScript;
    public Image hpBar;
    public GameObject hpCanvas;
    public int enemyHealth;
    int maxEnemyHealth;
    public float speed;
    public float attackRange;

    [Header("Explode")]
    public GameObject explodeEffect;
    public float fieldOfExplode;
    public float explodeForce;
    public int explodeDamage;
    public LayerMask layerToExplode;

    [Header("Shot")]
    AudioSource shotSound;

    public Transform shotPoint;
    public float timeBtwShots;
    float shotTime;

    [Header("Item")]
    public GameObject healPack;
    public GameObject speedUp;

    void Start()
    {
        maxEnemyHealth = enemyHealth;

        objectPool = FindObjectOfType<ObjectPool>();
        player = FindObjectOfType<Player>();

        shotSound = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyScript = GetComponent<Enemy>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        hpBar.fillAmount = (float)enemyHealth / maxEnemyHealth;
        Vector3 hpCanvasAngle = Vector3.zero;
        hpCanvas.transform.rotation = Quaternion.identity;

        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        direction.Normalize();
        
        rb.rotation = angle;
        movement = direction;

        if(player.cur_playerHealth > 0)
        {
            enemyScript.enabled = true;
        }
        else if(player.cur_playerHealth <= 0)
        {
            enemyScript.enabled = false;
            gameObject.SetActive(false);
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
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject wings = null;
        GameObject shield = null;
        GameObject attackWing = null;

        if(Vector3.Distance(player.transform.position, transform.position) <= attackRange) //만약 공격범위보다 가깝다면
        {
            switch(enemyType)
            {
                case Type.Normal:
                    Shot(angle, "N_EnemyBullet");
                    break;
                case Type.Giant:
                    Shot(angle, "G_EnemyBullet");
                    break;
                case Type.Explosive:
                    Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfExplode, layerToExplode);
                    foreach(Collider2D obj in objects)
                    {
                        Vector3 e_Direction = obj.transform.position - transform.position;
                        obj.GetComponent<Rigidbody2D>().AddForce(e_Direction * explodeForce);
                        Instantiate(explodeEffect, transform.position, Quaternion.identity);
                        player.TakeDamage(explodeDamage);
                        this.gameObject.SetActive(false);
                    }
                    break;
                case Type.Shield:
                    //방패 내리기
                    wings = transform.GetChild(0).gameObject;
                    attackWing = wings.transform.GetChild(0).gameObject;
                    shield = wings.transform.GetChild(1).gameObject;

                    shield.GetComponent<SpriteRenderer>().enabled = false;
                    shield.GetComponent<DefenseWeapon>().enabled = false;
                    shield.GetComponent<BoxCollider2D>().enabled = false;

                    attackWing.GetComponent<SpriteRenderer>().enabled = true;
                    
                    Shot(angle, "N_EnemyBullet");
                    break;
            }
        }
        else if(Vector3.Distance(player.transform.position, transform.position) > attackRange) //만약 공격범위보다 멀다면
        {
            if(enemyType == Type.Shield)
            {
                //방패 들기
                wings = transform.GetChild(0).gameObject;
                attackWing = wings.transform.GetChild(0).gameObject;
                shield = wings.transform.GetChild(1).gameObject;

                shield.GetComponent<SpriteRenderer>().enabled = true;
                shield.GetComponent<DefenseWeapon>().enabled = true;
                shield.GetComponent<BoxCollider2D>().enabled = true;

                attackWing.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    void Shot(float angle, string bulletType)
    {
        if (Time.time >= shotTime)
        {
            anim.Play("Enemy_Attack");
            shotSound.Play();
            GameObject bulletObj;
            bulletObj = objectPool.MakeObject(bulletType);
            bulletObj.transform.position = shotPoint.position;
            bulletObj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            shotTime = Time.time + timeBtwShots;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfExplode);
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
            this.gameObject.SetActive(false);
            
            int itemRandomNum = Random.Range(0,10);
            player.killEnemyCount++;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Invoke("Restore", 0.1f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            switch(enemyType)
            {
                case Type.Normal:
                    player.LevelScoreUP(3.0f);
                    break;
                case Type.Giant:
                    player.LevelScoreUP(5.0f);
                    break;
                case Type.Explosive:
                    player.LevelScoreUP(10f);
                    break;
                case Type.Shield:
                    player.LevelScoreUP(15f);
                    break;
            }

            if(itemRandomNum < 6)
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