using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShootUp_Item : Item
{
    Tilemap roomWall;
    public GameObject shootUpEffect;

    Player player;
    GameObject attackWingObj;

    void Start()
    {
        roomWall = GameObject.FindObjectOfType<Tilemap>();
        player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        attackWingObj = GameObject.FindGameObjectWithTag("AttackWing");
    }

    void Update()
    {
        if(player.timeBtwShots < 0.2)
        {
            roomWall.gameObject.GetComponent<Tilemap>().color = new Color(1f, 0.3f, 1f);
        }
        else
        {
            roomWall.gameObject.GetComponent<Tilemap>().color = Color.white;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RunItem();
        }
    }

    public override void RunItem()
    {
        #region shootup
        player.timeBtwShots /= 2;
        roomWall.gameObject.GetComponent<Tilemap>().color = new Color(1f, 0.3f, 1f);
        Instantiate(shootUpEffect, transform.position, Quaternion.identity);
        Invoke("ResetItemEffect", 5f);
        DestoryObject();
        #endregion
    }

    public override void ResetItemEffect()
    {
        #region shootup
        GameObject attackWingObj = GameObject.FindGameObjectWithTag("AttackWing");
        player.timeBtwShots *= 2;
        if(player.timeBtwShots >= 0.2f)
        {
            roomWall.gameObject.GetComponent<Tilemap>().color = Color.white;
        }
        #endregion
    }

    public void DestoryObject()
    {
        gameObject.SetActive(false);
    }
}
