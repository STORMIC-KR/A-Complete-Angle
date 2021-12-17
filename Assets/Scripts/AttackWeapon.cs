using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeapon : MonoBehaviour
{
    public Animator anim;

    public Transform shotPoint;
    public float timeBtwShots;
    private float shotTime;

    public AudioSource shootSound;

    public ObjectPool objectPool;
    public GameManager gameManager;

    // public GameObject FindClosestEnemy()
    // {
    //     GameObject[] enemies;
    //     enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //     GameObject closest = null;
    //     float distance = Mathf.Infinity;
    //     Vector3 position = transform.position;
    //     foreach(GameObject enemy in enemies)
    //     {
    //         Vector3 diff = enemy.transform.position - position;
    //         float curDistance = diff.sqrMagnitude;
    //         if(curDistance < distance)
    //         {
    //             closest = enemy;
    //             distance = curDistance;
    //         }
    //     }
    //     return closest;
    // }

    void Start() 
    {
        shootSound = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (Input.GetMouseButton(0))
            {
                if (Time.time >= shotTime)
                {
                    anim.Play("Player_Attack");
                    shootSound.Play();
                    GameObject bullet = objectPool.MakeObject("Bullet");
                    bullet.transform.position = shotPoint.position;
                    bullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    shotTime = Time.time + timeBtwShots;
                }
            }
        }
    }

    // public void Mobile_Attack()
    // {
    //     if(SystemInfo.deviceType == DeviceType.Handheld)
    //     {
    //         Vector2 direction = FindClosestEnemy().transform.position - transform.position;
    //         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //         if (Time.time >= shotTime)
    //         {
    //             anim.Play("Player_Attack");
    //             shootSound.Play();
    //             GameObject bullet = objectPool.MakeObject("Bullet");
    //             bullet.transform.position = shotPoint.position;
    //             bullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    //             shotTime = Time.time + timeBtwShots;
    //         }
    //     }
    // }
}
