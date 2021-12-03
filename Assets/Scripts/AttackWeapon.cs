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

    void Start() 
    {
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {

    }

    void Attack()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                anim.Play("Attack");
                shootSound.Play();
                GameObject bullet = objectPool.MakeObject("Bullet");
                bullet.transform.position = shotPoint.position;
                bullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                shotTime = Time.time + timeBtwShots;
            }
        }
    }
}
