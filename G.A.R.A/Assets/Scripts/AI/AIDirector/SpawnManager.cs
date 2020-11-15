﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
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
    public float timeBetweenMobs = 10f;
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
        if (mobSpawnTimer > timeBetweenMobs)
        {

            highIntensityEnemyCount = Mathf.Min(highIntensityEnemyCount + 5, 50);
            lowIntensityEnemyCount = Mathf.Min(lowIntensityEnemyCount + 2, 20);
            mobSize += 1;
        }
    }

    private void Update()
    {
        if (!started)
            return;

        mobSpawnTimer += Time.deltaTime;
        if (mobSpawnTimer > timeBetweenMobs && BoidManager.allBoids.Count < highIntensityEnemyCount && (currentTempo == Pacer.TempoType.BUILDUP || currentTempo == Pacer.TempoType.SUSTAIN))
        {
            mobReady = true;
            mobSpawnTimer = 0;
        }
    }

    public void OnPlayerNodeChange(Node playerNode, List<Node> oldActiveArea, List<Node> newActiveArea, Vector3 playerPos)
    {
        List<Node> newActiveAreaDiff = new List<Node>(newActiveArea);
        List<Node> oldActiveAreaDiff = new List<Node>(oldActiveArea);
        RemoveCommonNodes(ref oldActiveAreaDiff, ref newActiveAreaDiff);
        PurgeEnemies(oldActiveAreaDiff, playerPos);
        SpawnWanderers(newActiveAreaDiff);
    }

    private void SpawnWanderers(List<Node> newActiveAreaDiff)
    {
        if (BoidManager.allBoids.Count < highIntensityEnemyCount)
        {
            for (int i = 0; i < newActiveAreaDiff.Count * 2; i++)
            {
                newActiveAreaDiff[i % newActiveAreaDiff.Count].spawner.Spawn(enemyPrefabs[i % enemyPrefabs.Length]);
                if (BoidManager.allBoids.Count > highIntensityEnemyCount)
                    break;
            }
        }
    }

    private void PurgeEnemies(List<Node> nodesOutOfRange, Vector3 playerPos)
    {

        for (int j = BoidManager.allBoids.Count - 1; j >= 0; j--)
        {
            if (Vector3.Distance(BoidManager.allBoids[j].transform.position, playerPos) > 50)
            {
                BoidManager.allBoids[j].GetComponent<EnemyStats>().Die();
            }
        }

    }

    private void RemoveCommonNodes(ref List<Node> oldActiveArea, ref List<Node> newActiveArea)
    {
        for (int i = oldActiveArea.Count - 1; i >= 0; i--)
        {
            Node oldCompare = oldActiveArea[i];
            for (int j = newActiveArea.Count - 1; j >= 0; j--)
            {
                Node newCompare = newActiveArea[j];
                if (oldCompare == newCompare)
                {
                    oldActiveArea.Remove(oldCompare);
                    newActiveArea.Remove(newCompare);
                    break;
                }
            }
        }
    }

    public void SpawnMob(Path path)
    {
        mobReady = false;
        mobSpawnTimer = 0;
        List<Edge.DoorType> doorTypes = new List<Edge.DoorType>();
        while (path.edges.Count > 0)
        {
            Edge temp = path.edges.Pop();
            switch (temp.type)
            {
                case Edge.DoorType.NONE:
                    break;
                case Edge.DoorType.INTERACT:
                    break;
                case Edge.DoorType.DESTRUCTABLE:
                case Edge.DoorType.TOGGLEABLE:
                    doorTypes.Add(temp.type);
                    break;
                default:
                    break;
            }
        }
        Node spawnNode = path.nodes.Peek();
        for (int i = 0; i < 3; i++)
        {
            if (path.nodes.Count > 0)
            {
                spawnNode = path.nodes.Pop();
            }
        }
        if (BoidManager.allBoids.Count < highIntensityEnemyCount)
            for (int i = 0; i < mobSize; i++)
            {
                spawnNode.spawner.Spawn(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)]);
            }

    }
}

