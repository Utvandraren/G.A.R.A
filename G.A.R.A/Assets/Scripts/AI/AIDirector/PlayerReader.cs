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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlayerHP()
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
        return false;
    }

    internal int GetHPPercent()
    {
        return playerStats.health / playerStats.startingHealth;
    }
    internal int GetShieldPercent()
    {
        return playerStats.shield / playerStats.maxShield;
    }
}
