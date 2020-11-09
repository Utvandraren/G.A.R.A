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
    public Pacer.TempoType currentTempo;
    public List<Edge.DoorType> obstacles;
    public int highIntensityEnemyCount = 10;
    public int lowIntensityEnemyCount = 0;
    public int mobSize = 5;
    private float mobSpawnTimer;
    public float timeBetweenMobs;
    Path optimalPath;
    List<Spawner> spawners;
    public bool started;
    public bool mobReady;
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

    private void Update()
    {
        if (!started)
            return;

        mobSpawnTimer += Time.deltaTime;
        if (mobSpawnTimer > timeBetweenMobs && BoidManager.allBoids.Count < highIntensityEnemyCount)
        {
            mobReady = true;
        }
    }

    public void SpawnStragelers(Node playerNode, List<Node> activeArea)
    {

    }

    public void SpawnMob(Node playerNode, List<Node> activeArea, List<Edge.DoorType> obstacleTypes)
    {
        mobReady = false;
        mobSpawnTimer = 0;
    }
}

