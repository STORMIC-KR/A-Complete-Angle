using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Heal_Item : Item
{
    public Tilemap roomWall;
    public int healValue;

    public ObjectPool objectPool;
    public GameObject healEffect;

    void Start()
    {
        roomWall = GameObject.FindObjectOfType<Tilemap>();
        objectPool = FindObjectOfType<ObjectPool>();
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
                roomWall.gameObject.GetComponent<Tilemap>().color = new Color(0.3f, 1f, 0.3f);
                player.cur_playerHealth += healValue;
                Instantiate(healEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                Invoke("ResetItemEffect", 3f);

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

    public override void ResetItemEffect()
    {
        roomWall.gameObject.GetComponent<Tilemap>().color = Color.white;
    }
}
