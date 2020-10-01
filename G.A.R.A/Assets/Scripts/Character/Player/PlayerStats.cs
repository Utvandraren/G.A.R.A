using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    int shield;
    [SerializeField] int maxShield;
    [SerializeField] float shieldRechargeDelay;
    [SerializeField] int shieldRechargeRate;
    float timer;

    protected override void Start()
    {
        base.Start();
        shield = maxShield;
    }

    private void Update()
    {
        if (shield == maxShield)
        {
            timer = 0;
            return;
        }
        if (timer >= shieldRechargeDelay)
            RechargeShield();
        else
            timer += Time.deltaTime;
    }

    private void RechargeShield()
    {
        shield += shieldRechargeRate;
        shield = Mathf.Min(shield, maxShield);
    }
    public override void TakeDamage(SciptableAttackObj attack)
    {
        timer = 0;
        if(shield == 0)
            base.TakeDamage(attack);
        else
        {
            shield -= attack.damage;
            shield = Mathf.Max(shield, 0);
        }
    }
    public void RestoreHealth(int amount)
    {
        //TODO: not pick up hp items if at full, need item implementation to do so
        health += amount;
        health = Mathf.Min(health, startingHealth);
    }
    public override void Die()
    {
        base.Die();
        Debug.Log("Player died");
        //Gamemanager.tooglegameoverscreen/loosegame();   <-- To be added later
    }
}
