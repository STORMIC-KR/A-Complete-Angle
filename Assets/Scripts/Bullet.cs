using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    
    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }
    
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.CompareTag("Enemy_Bullet"))
        {
            if(other.CompareTag("Player"))
            {
                DestroyBullet();
            }
            else if(other.CompareTag("RoomWall"))
            {
                DestroyBullet();
            }
        }
        else if(gameObject.CompareTag("Bullet"))
        {
            if(other.CompareTag("Enemy"))
            {
                DestroyBullet();
            }
            else if(other.CompareTag("RoomWall"))
            {
                DestroyBullet();
            }
        }
    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
