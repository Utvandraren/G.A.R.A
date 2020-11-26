using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReader : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;
    [SerializeField]SciptableIntObj laserAmmo, projectileAmmo, tazerAmmo;
    int playerNode;
    bool inCombat;
    float combatTimer = 0;
    float timeToNotBeInCombat = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        playerStats.tookDamage += PlayerStats_tookDamage;
    }

    private void PlayerStats_tookDamage(object player, TakeDamageEventArgs eventArgsDamage)
    {
        inCombat = true;
    }

    private void Update()
    {
        if (inCombat)
        {
            combatTimer += Time.deltaTime;
            if (combatTimer > timeToNotBeInCombat)
                inCombat = false;
        }
        
    }

    public float GetPlayerHP()
    {
        return playerStats.health;
    }

    public bool PlayerChangedNode(int currentNode)
    {
        if (currentNode == playerNode)
            return false;
        else
        {
            playerNode = currentNode;
            return true;
        }
    }

    internal bool InCombat()
    {
        return inCombat;
    }

    internal float GetHPPercent()
    {
        return playerStats.health / playerStats.startingHealth;
    }
    internal float GetShieldPercent()
    {
        return playerStats.shield / playerStats.maxShield;
    }
}
