using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject bullet_Prefab;
    public GameObject n_enemy_Bullet_Prefab;
    public GameObject g_enemy_Bullet_Prefab;
    public GameObject normal_Enemy_Prefab;
    public GameObject tanker_Enemy_Prefab;
    public GameObject healPack_Prefab;
    public GameObject speedUp_Prefab;

    GameObject[] bullet;
    GameObject[] n_enemy_Bullet;
    GameObject[] g_enemy_Bullet;
    GameObject[] normal_Enemy;
    GameObject[] tanker_Enemy;
    GameObject[] healPack;
    GameObject[] speedUp_Item;

    GameObject[] targetPool;

    public int bulletNum;
    public int n_BulletNum;
    public int g_BulletNum;
    public int n_EnemyNum;
    public int g_EnemyNum;
    public int healPackNum;
    public int speedPackNum;

    void Awake()
    {
        bullet = new GameObject[bulletNum];
        n_enemy_Bullet = new GameObject[n_BulletNum];
        g_enemy_Bullet = new GameObject[g_BulletNum];
        normal_Enemy = new GameObject[n_EnemyNum];
        tanker_Enemy = new GameObject[g_EnemyNum];
        healPack = new GameObject[healPackNum];
        speedUp_Item = new GameObject[speedPackNum];

        Generate();
    }

    void Generate()
    {
        for(int index = 0; index < bullet.Length; index++)
        {
            bullet[index] = Instantiate(bullet_Prefab);
            bullet[index].SetActive(false);
        }

        for(int index = 0; index < n_enemy_Bullet.Length; index++)
        {
            n_enemy_Bullet[index] = Instantiate(n_enemy_Bullet_Prefab);
            n_enemy_Bullet[index].SetActive(false);
        }

        for(int index = 0; index < g_enemy_Bullet.Length; index++)
        {
            g_enemy_Bullet[index] = Instantiate(g_enemy_Bullet_Prefab);
            g_enemy_Bullet[index].SetActive(false);
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

        for(int index = 0; index < speedUp_Item.Length; index++)
        {
            speedUp_Item[index] = Instantiate(speedUp_Prefab);
            speedUp_Item[index].SetActive(false);
        }
    }

    public GameObject MakeObject(string type)
    {
        switch(type)
        {
            case "Bullet":
                targetPool = bullet;
                break;
            case "N_EnemyBullet":
                targetPool = n_enemy_Bullet;
                break;
            case "G_EnemyBullet":
                targetPool = g_enemy_Bullet;
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
            case "ShootUp":
                targetPool = speedUp_Item;
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
