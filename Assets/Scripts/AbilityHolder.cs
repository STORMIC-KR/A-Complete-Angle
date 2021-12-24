using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability dashAbility;
    float coolDownTime;
    float activeTime;
    public GameManager gameManager;
    public AudioSource dashSound;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        switch(state)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    dashAbility.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = dashAbility.activeTime;
                    dashSound.Play();
                }
            break;
            case AbilityState.active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    dashAbility.BeginCoolDown(gameObject);
                    state = AbilityState.cooldown;
                    coolDownTime = dashAbility.coolDownTime;
                }
            break;
            case AbilityState.cooldown:
                if(coolDownTime > 0)
                {
                    coolDownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
            break;
        }
    }
}
