using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUp_Item : ItemTest
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

        #region speedup
        // GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        // Player playerScript = playerObj.GetComponent<Player>();
        // playerScript.normalAcceleration *= increasement;
        // Instantiate(speedUpEffect, transform.position, Quaternion.identity);
        // DestoryObject();
        // Invoke("ResetItemEffect", 5f);
        #endregion        
    }

    public override void ResetItemEffect()
    {
        #region shootup
        GameObject attackWingObj = GameObject.FindGameObjectWithTag("AttackWing");
        AttackWeapon attackWeapon = attackWingObj.GetComponent<AttackWeapon>();
        attackWeapon.timeBtwShots *= 2;
        #endregion

        #region speedup
        // GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        // Player playerScript = playerObj.GetComponent<Player>();
        // playerScript.normalAcceleration /= increasement;
        #endregion
    }

    public void DestoryObject()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RunItem();
        }
    }
}