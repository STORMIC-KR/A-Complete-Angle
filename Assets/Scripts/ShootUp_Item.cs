using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShootUp_Item : Item
{
    public GameObject shootUpEffect;

    public override void RunItem()
    {
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
