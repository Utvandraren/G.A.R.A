using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : Stats
{
    public SciptableIntObj bossHealth;
    [SerializeField] private GameObject bossWinTimeLine;
    
    [HideInInspector]
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
        base.TakeDamage(attack);

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
        //if(bossHealth.value <= 0f)
        //{
        //    Die();
        //}
    }

    public override void Die()
    {
        //GameManager.Instance.Win();
        //StartTimelineanimation
        bossWinTimeLine.SetActive(true);

    }
}
