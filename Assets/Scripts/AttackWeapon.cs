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
                    GameObject bullet = objectPool.MakeObject("Bullet");
                    anim.Play("Player_Attack");
                    shootSound.Play();
                    bullet.transform.position = shotPoint.position;
                    bullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    shotTime = Time.time + timeBtwShots;
                }
            }
        }
    }
}
