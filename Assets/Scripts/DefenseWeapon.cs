using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWeapon : MonoBehaviour
{
    public GameObject shieldEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy_Bullet"))
        {
            Instantiate(shieldEffect, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
        }
    }
}
