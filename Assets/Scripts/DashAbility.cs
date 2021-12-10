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
        
        player.acceleration = dashVelocity;
    }
    
    public override void BeginCoolDown(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

        player.acceleration = player.normalAcceleration;
    }
}
