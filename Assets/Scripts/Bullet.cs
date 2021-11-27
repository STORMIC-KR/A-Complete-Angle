using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;
    public float speed;
    public float lifeTime;
    public int damage;
    
    void Start()
    {
        targetPlayer = GameObject.Find("Player").transform;
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
        Debug.Log("Bullet Destroyed");
    }
}
