using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SpawnManager : MonoBehaviour
{
    public enum SpawnType
    {
        NORMAL,
        HALTING,
        PUSHING,
    }

    public int highIntensityEnemyCount = 10;
    public int lowIntensityEnemyCount = 0;
    public int mobSize = 5;

    Path optimalPath;
    List<Spawner> spawners;
    int playerRoomIndex = 0;
    private void Start()
    {
        GetComponentsInChildren(spawners);
    }

    public void Initialize()
    {
        
    }

    public void IncreaseThreatSizes()
    {

    }

    public void RecieveMission(Vector3 playerPos, GameObject[] enemyBatch, SpawnType type)
    {
        /*
         * 1. Find player room index
         * 2. Find spawnlocations
         * 3. Delegate enemy spawns
         */
    }
}

