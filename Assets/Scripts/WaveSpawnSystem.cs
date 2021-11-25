using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnSystem : MonoBehaviour
{
    [SerializeField] private float spawnRadius = 5f;

    public string[] enemies;
    public ObjectPool objectPool;

    public float spawnRate = 1.0f;
    public float timeBtwWaves = 3.0f;

    public int spawningEnemyCount;
    bool waveIsDone = true;


    void Awake()
    {
        enemies = new string[] { "NEnemy", "TEnemy" };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

            Vector2 spawnPos = GameObject.Find("Player").transform.position;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

            GameObject enemy = objectPool.MakeObject(enemies[ranEnemy]);
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(spawnRate);
        }

        waveIsDone = true;
        spawnRate -= 0.1f;
        spawningEnemyCount += 1;

        yield return new WaitForSeconds(timeBtwWaves);
    }
}
