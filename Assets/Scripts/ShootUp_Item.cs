using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShootUp_Item : Item
{
    public GameObject shootUpEffect;

    Player player;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
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
        Instantiate(shootUpEffect, transform.position, Quaternion.identity);
        Invoke("ResetItemEffect", 5f);
        DestoryObject();
        #endregion
    }

    public override void ResetItemEffect()
    {
        #region shootup
        player.timeBtwShots *= 2;
        #endregion
    }

    public void DestoryObject()
    {
        gameObject.SetActive(false);
    }
}
