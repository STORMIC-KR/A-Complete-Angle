using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    public int healValue;

    public ObjectPool objectPool;
    public GameObject healEffect;

    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(other.CompareTag("Player"))
        {
            if(player.cur_playerHealth < player.max_playerHealth)
            {
                player.cur_playerHealth += healValue;
                Instantiate(healEffect, transform.position, Quaternion.identity);
                Debug.Log("Eat HP!");
                gameObject.SetActive(false);

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
    }
}
