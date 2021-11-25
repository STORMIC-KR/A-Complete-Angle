using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    public int healValue;

    public ObjectPool objectPool;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(other.CompareTag("Player"))
        {
            if(player.cur_playerHealth < player.max_playerHealth)
            {
                player.cur_playerHealth += healValue;

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
