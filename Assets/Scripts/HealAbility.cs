using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealAbility : Ability
{
    public int healValue;

    public override void Activate(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();

        if(player.cur_playerHealth < player.max_playerHealth)
        {
            sr.color = new Color(0.2f, 1f, 0.2f, 1f);
            player.cur_playerHealth += healValue;
            Debug.Log("Heal Activate");
            if(player.cur_playerHealth >= player.max_playerHealth)
            {
                player.cur_playerHealth = player.max_playerHealth;
            }
        }
        else
        {
            return;
        }
    }

    public override void BeginCoolDown(GameObject parent)
    {
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();
        sr.color = new Color(0.2f, 1f, 1f, 1f);

        Debug.Log("CoolDown");
    }
}
