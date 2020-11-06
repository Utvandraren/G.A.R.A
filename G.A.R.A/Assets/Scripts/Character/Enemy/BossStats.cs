using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : Stats
{
    public SciptableIntObj bossHealth;
    public bool isInvicible;

    int treshold;

    protected override void Start()
    {
        treshold = bossHealth.startValue / 3;
        health = bossHealth.startValue;
        isInvicible = false;
    }


    public override void TakeDamage(SciptableAttackObj attack)
    {
        if(isInvicible)
        {
            return;
        }
        bossHealth.value -= attack.damage;
        treshold -= attack.damage;

        if (treshold <= 0)
        {
            treshold = bossHealth.startValue / 3;
            GetComponent<BossManager>().PrepareNextPhase();  
        }
    }

    public override void Die()
    {
        GameManager.Instance.Win();
        //StartTimelineanimation
        

    }
}
