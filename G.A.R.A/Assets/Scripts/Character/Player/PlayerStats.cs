using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Stats
{
    public float shield;
    public float sprint;
    [SerializeField] public int maxShield;
    [SerializeField] public int maxSprint;
    [SerializeField] private float shieldRechargeDelay;
    [SerializeField] private float shieldRechargeRate;
    [SerializeField] private float sprintRechargeDelay;
    [SerializeField] private float sprintRechargeRate;
    [SerializeField] private float sprintUsageRate;

    private float shieldTimer;
    private float sprintTimer;

    private float sprintRegenThreshold = 0.15f;
    public bool canSprint { get; private set; }

    private PlayerController player;

    public delegate void PlayerDamaged(object player, TakeDamageEventArgs eventArgsDamage);
    public event PlayerDamaged tookDamage;

    protected override void Start()
    {
        base.Start();
        shield = maxShield;
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (shield == maxShield)
        {
            shieldTimer = 0;
        }
        if (shieldTimer >= shieldRechargeDelay)
            RechargeShield();
        else
            shieldTimer += Time.deltaTime;


        if(player.isSprinting)
        {
            DrainSprint();
            sprintTimer = 0;
        }
        if (sprintTimer >= sprintRechargeDelay && !player.isSprinting)
            RechargeSprint();
        else sprintTimer += Time.deltaTime;

        if (sprint / maxSprint <= sprintRegenThreshold && !player.isSprinting)
        {
            canSprint = false;
        }
        else canSprint = true;
    }

    private void RechargeShield()
    {
        shield += shieldRechargeRate * Time.deltaTime;
        shield = Mathf.Min(shield, maxShield);
    }

    private void RechargeSprint()
    {
        sprint += sprintRechargeRate * Time.deltaTime;
        sprint = Mathf.Min(sprint, maxSprint);
    }

    private void DrainSprint()
    {
        sprint -= sprintUsageRate * Time.deltaTime;
        sprint = Mathf.Min(sprint, maxSprint);
    }

    public override void TakeDamage(SciptableAttackObj attack)
    {
        shieldTimer = 0;
        if(shield == 0)
            base.TakeDamage(attack);
        else
        {
            shield -= attack.damage;
            shield = Mathf.Max(shield, 0);
        }
        tookDamage?.Invoke(this, new TakeDamageEventArgs(attack.damage));
    }

    public override void TakeContinuousDamage(SciptableAttackObj attack)
    {
        shieldTimer = 0;
        if (shield == 0)
            base.TakeContinuousDamage(attack);
        else
        {
            shield -= (attack.damage * Time.deltaTime);
            shield = Mathf.Max(shield, 0);
        }
        tookDamage?.Invoke(this, new TakeDamageEventArgs(attack.damage));
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
        GameManager.Instance.GameOver();
    }

    public void ResetStats()
    {
        shield = maxShield;
        health = startingHealth;
        GetComponent<WeaponManager>().ResetAllAmmo();
    }
}
