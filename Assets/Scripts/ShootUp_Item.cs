using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShootUp_Item : Item
{
    public Tilemap roomWall;
    public GameObject shootUpEffect;

    void Start()
    {
        roomWall = GameObject.FindObjectOfType<Tilemap>();
    }

    public override void RunItem()
    {
        roomWall.gameObject.GetComponent<Tilemap>().color = new Color(1f, 0.3f, 1f);
        Color shootColor = new Color(1f, 0.3f, 1f);

        #region shootup
        GameObject attackWingObj = GameObject.FindGameObjectWithTag("AttackWing");
        AttackWeapon attackWeapon = attackWingObj.GetComponent<AttackWeapon>();
        attackWeapon.timeBtwShots /= 2;
        Instantiate(shootUpEffect, transform.position, Quaternion.identity);
        Invoke("ResetItemEffect", 5f);
        DestoryObject();
        #endregion
    }

    public override void ResetItemEffect()
    {
        roomWall.gameObject.GetComponent<Tilemap>().color = Color.white;

        #region shootup
        GameObject attackWingObj = GameObject.FindGameObjectWithTag("AttackWing");
        AttackWeapon attackWeapon = attackWingObj.GetComponent<AttackWeapon>();
        attackWeapon.timeBtwShots *= 2;
        #endregion
    }

    public void DestoryObject()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RunItem();
        }
    }
}
