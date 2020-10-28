using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : Stats
{
    public SciptableIntObj bossHealth;

    int treshold;

    protected override void Start()
    {
        treshold = bossHealth.startValue / 3;
        health = bossHealth.startValue;
    }

    
    public override void TakeDamage(SciptableAttackObj attack)
    {
        bossHealth.value -= attack.damage;
        treshold -= attack.damage;

        if(treshold <= 0)
        {
            treshold = bossHealth.startValue / 3;
            GetComponent<BossManager>().TransitionToNextPhase();
        }
    }

    public override void Die()
    {
        GameManager.Instance.Win();
        //StartTimelineanimation
        

    }
}
