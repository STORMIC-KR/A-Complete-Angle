using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp_Item : ItemTest
{
    public int increasement;

    public override void RunItem()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Player playerScript = playerObj.GetComponent<Player>();
        playerScript.normalAcceleration *= increasement;
        DestoryObject();
        Invoke("ResetItemEffect", 5f);
    }

    public override void ResetItemEffect()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Player playerScript = playerObj.GetComponent<Player>();
        playerScript.normalAcceleration /= increasement;
    }

    public void DestoryObject()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            RunItem();
        }
    }
}
