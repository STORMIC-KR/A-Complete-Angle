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

    public Text waveText;
    public int waveNum = 0;

    public float spawnRate = 1.0f;
    public float timeBtwWaves = 3.0f;

    public int spawningEnemyCount;
    bool waveIsDone = true;


    void Awake()
    {
        enemies = new string[] { "NEnemy", "TEnemy" };
    }
    
    void Update()
    {
        waveText.text = "Wave \n" + waveNum;
        if(waveIsDone == true)
        {
            StartCoroutine(WaveSpawn());
        }
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

            GameObject enemy = objectPool.MakeObject(enemies[ranEnemy]);
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(spawnRate);
        }

        waveNum += 1;
        waveIsDone = true;
        spawnRate -= 0.1f;
        spawningEnemyCount += 1;

        yield return new WaitForSeconds(timeBtwWaves);
    }
}
