using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability1;

    float coolDownTime;
    float activeTime;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;


    void Update()
    {
        switch(state)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    ability1.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability1.activeTime;
                }
            break;
            case AbilityState.active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability1.BeginCoolDown(gameObject);
                    state = AbilityState.cooldown;
                    coolDownTime = ability1.coolDownTime;
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
