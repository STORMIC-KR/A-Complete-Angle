using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;
    public float speed;
    public float lifeTime;
    public int damage;

    public float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Invoke("DestroyBullet", lifeTime);
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if(Vector3.Distance(targetPlayer.position, transform.position) >= maxDistance)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.CompareTag("Enemy_Bullet"))
        {
            if(other.CompareTag("Player"))
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
        }
    }

    void DestroyBullet()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Bullet Destroyed");
    }
}
