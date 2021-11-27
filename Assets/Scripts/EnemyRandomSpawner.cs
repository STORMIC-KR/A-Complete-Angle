using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius = 5f, time = 3f;

    public string[] enemies;
    public ObjectPool objectPool;

    GameObject[] enemyCount;


    void Awake()
    {
        enemies = new string[] { "NEnemy", "TEnemy" };
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
    }

    IEnumerator SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, enemies.Length);

        Vector2 spawnPos = GameObject.Find("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        GameObject enemy = objectPool.MakeObject(enemies[ranEnemy]);
        enemy.transform.position = spawnPos;
        enemy.transform.rotation = Quaternion.identity;
        
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnEnemy());
    }
}
