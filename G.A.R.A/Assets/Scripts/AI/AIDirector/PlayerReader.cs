using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReader : MonoBehaviour
{
    public GameObject player;
    PlayerStats playerStats;
    [SerializeField]SciptableIntObj laserAmmo, projectileAmmo, tazerAmmo;
    int playerNode;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
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
}
