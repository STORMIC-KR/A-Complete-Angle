using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawnSystem : MonoBehaviour
{
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float maxX, minX, maxY, minY;

    public string[] enemies;
    public ObjectPool objectPool;
    public GameObject player;
    public GameObject spawnEffect;

    public Text waveText;
    
    public int waveNum = 1;
    public int spawningEnemyCount;

    public float spawnRate = 1.0f;
    public float timeBtwWaves = 3.0f;

    bool waveIsDone = false;


    void Awake()
    {
        enemies = new string[] { "NEnemy", "TEnemy", "EEnemy" };
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("TurnWaveON", 3f);
    }
    
    void Update()
    {
        waveText.text = "Wave \n" + waveNum;
        if(waveIsDone == true)
        {
            StartCoroutine(WaveSpawn());
        }

        if(player.GetComponent<Player>().cur_playerHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void TurnWaveON()
    {
        waveIsDone = true;
    }

    IEnumerator WaveSpawn()
    {
        waveIsDone = false;

        for(int i = 0; i < spawningEnemyCount; i++)
        {
            
            int ranEnemy = Random.Range(0, enemies.Length);
            float ranX_pos = Random.Range(minX, maxX);
            float ranY_pos = Random.Range(minY, maxY);

            Vector2 spawnPosition = new Vector2(ranX_pos, ranY_pos);
            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

            if(spawnPosition != playerPosition)
            {
                Instantiate(spawnEffect, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                GameObject enemy = objectPool.MakeObject(enemies[ranEnemy]);
                enemy.transform.position = spawnPosition;

                yield return new WaitForSeconds(spawnRate);
            }
        }

        waveNum += 1;
        waveIsDone = true;
        spawnRate -= 0.1f;
        spawningEnemyCount += 3;

        yield return new WaitForSeconds(timeBtwWaves);
    }
}
