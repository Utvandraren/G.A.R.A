﻿using System;
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
    public override void Die()
    {
        base.Die();
        Debug.Log("Player died");
        //Gamemanager.tooglegameoverscreen/loosegame();   <-- To be added later
    }
}
