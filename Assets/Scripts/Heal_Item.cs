using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Item : Item
{
    public int healValue;

    public ObjectPool objectPool;
    public GameObject healEffect;

    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }
    
    void Update()
    {
        Invoke("DestroyItem", 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RunItem();
        }
    }

    public override void RunItem()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player.cur_playerHealth < player.max_playerHealth)
            {
                player.cur_playerHealth += healValue;
                Instantiate(healEffect, transform.position, Quaternion.identity);
                DestroyItem();

                if(player.cur_playerHealth >= player.max_playerHealth)
                {
                    player.cur_playerHealth = player.max_playerHealth;
                }
            }
            else
            {
                return;
            }
    }

    public override void ResetItemEffect(){}

    void DestroyItem()
    {
        gameObject.SetActive(false);
    }
}
