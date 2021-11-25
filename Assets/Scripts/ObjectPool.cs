using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject bullet_Prefab;
    public GameObject enemy_Bullet_Prefab;
    public GameObject normal_Enemy_Prefab;
    public GameObject tanker_Enemy_Prefab;
    public GameObject healPack_Prefab;

    GameObject[] bullet;
    GameObject[] enemy_Bullet;
    GameObject[] normal_Enemy;
    GameObject[] tanker_Enemy;
    GameObject[] healPack;

    GameObject[] targetPool;

    void Awake()
    {
        bullet = new GameObject[10];
        enemy_Bullet = new GameObject[50];
        normal_Enemy = new GameObject[50];
        tanker_Enemy = new GameObject[50];
        healPack = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        for(int index = 0; index < bullet.Length; index++)
        {
            bullet[index] = Instantiate(bullet_Prefab);
            bullet[index].SetActive(false);
        }

        for(int index = 0; index < enemy_Bullet.Length; index++)
        {
            enemy_Bullet[index] = Instantiate(enemy_Bullet_Prefab);
            enemy_Bullet[index].SetActive(false);
        }
        
        for(int index = 0; index < normal_Enemy.Length; index++)
        {
            normal_Enemy[index] = Instantiate(normal_Enemy_Prefab);
            normal_Enemy[index].SetActive(false);
        }

        for(int index = 0; index < tanker_Enemy.Length; index++)
        {
            tanker_Enemy[index] = Instantiate(tanker_Enemy_Prefab);
            tanker_Enemy[index].SetActive(false);
        }

        for(int index = 0; index < healPack.Length; index++)
        {
            healPack[index] = Instantiate(healPack_Prefab);
            healPack[index].SetActive(false);
        }
    }

    public GameObject MakeObject(string type)
    {
        switch(type)
        {
            case "Bullet":
                targetPool = bullet;
                break;
            case "EnemyBullet":
                targetPool = enemy_Bullet;
                break;
            case "NEnemy":
                targetPool = normal_Enemy;
                break;
            case "TEnemy":
                targetPool = tanker_Enemy;
                break;
            case "HealPack":
                targetPool = healPack;
                break;
        }

        for(int index = 0; index < targetPool.Length; index++)
        {
            if(!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}
