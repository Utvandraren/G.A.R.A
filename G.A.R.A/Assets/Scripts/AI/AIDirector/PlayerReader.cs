using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReader : MonoBehaviour
{
    //[SerializeField] private SciptableIntObj laserAmmo, projectileAmmo, tazerAmmo;
    [HideInInspector] public GameObject player;
    [HideInInspector] public PlayerStats playerStats;
    private int playerNode;
    private bool inCombat;
    private float combatTimer = 0;
    private float timeToNotBeInCombat = 1f;
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
        combatTimer = 0;
                Debug.Log("in Combat");
    }

    private void Update()
    {
        if (inCombat)
        {
            combatTimer += Time.deltaTime;
            if (combatTimer > timeToNotBeInCombat)
            {
                combatTimer = 0;
                Debug.Log("No longer in Combat");
                inCombat = false;
            }
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
