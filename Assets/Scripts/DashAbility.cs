using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);

        //rb.velocity = player.movementInput.normalized * dashVelocity;
        player.acceleration = dashVelocity;
        parent.GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log("Dash Activate");
    }
    
    public override void BeginCoolDown(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

        player.acceleration = player.normalAcceleration;
        parent.GetComponent<CircleCollider2D>().enabled = true;
        Debug.Log("CoolDown");
    }
}
