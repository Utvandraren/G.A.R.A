using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum SpawnType
    {
        NORMAL,
        HALTING,
        PUSHING,
    }

    [Header("Global Settings")]
    public GameObject[] enemyPrefabs;
    public int purgeDistance = 150;
    public float timeBetweenScaleUp = 10f;
    [SerializeField] private int nrSwarmerPerSpawn = 2;
    [SerializeField] private int stragelerGroupSize = 2;

    [Header("High Intensity Settings")]
    public int intenseEnemyMax = 50;
    public int intenseEnemyAmount = 10;
    public int highScaleAmount = 2;
    [Header("Low Intensity Settings")]
    public int calmEnemyMax = 10;
    public int calmEnemyAmount = 0;
    public int lowScaleAmount = 2;
    [Header("Mob Spawning settings Settings")]
    public int mobMaxSize = 10;
    public int mobSize = 5;
    public int mobScaleAmount = 2;
    [Tooltip("First mob spawns from the average of max and min")]
    [SerializeField] private float mobMinSpawnInterval = 7f;
    [Tooltip("First mob spawns from the average of max and min")]
    [SerializeField] private float mobMaxSpawnInterval = 20f;
    private float timeBetweenMobs;
    [SerializeField] private int nrNodesAway = 3;


    private bool active;
    [HideInInspector] public bool mobReady;
    [HideInInspector] public Pacer.TempoType currentTempo;

    private float scalingTimer;
    private int activeIntensityEnemyMax = 10;
    private float mobSpawnTimer;
    private List<Spawner> spawners;

    private void PrintSettings()
    {
        StreamWriter streamWriter = new StreamWriter("panic.txt", true);
        streamWriter.WriteLine(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + " " + DateTime.Now.ToString());
        streamWriter.WriteLine(string.Format("Straggeler spawns: {0}, High Max: {1}, High Start {2}; Low Max: {3}, Low Start: {4}; Mob Max {5}, Mob Start {6}",
                                                stragelerGroupSize, intenseEnemyMax, intenseEnemyAmount, calmEnemyMax, calmEnemyAmount, mobMaxSize, mobSize));
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void Activate()
    {
        active = true;
    }
    public void Deactivate()
    {
        active = false;
    }

    private void Start()
    {
        GetComponentsInChildren(spawners);
        timeBetweenMobs = (mobMinSpawnInterval + mobMaxSpawnInterval) / 2f;
        PrintSettings();
    }

    public void IncreaseThreatSizes()
    {
        intenseEnemyAmount = Mathf.Min(intenseEnemyAmount + highScaleAmount, intenseEnemyMax);
        calmEnemyAmount = Mathf.Min(calmEnemyAmount + lowScaleAmount, calmEnemyMax);
        mobSize = Mathf.Min(mobSize + mobScaleAmount, mobMaxSize);
    }

    private void Update()
    {
        if (!active)
            return;
        switch (currentTempo)
        {
            case Pacer.TempoType.BUILDUP:
            case Pacer.TempoType.SUSTAIN:
                activeIntensityEnemyMax = intenseEnemyAmount;
                break;
            case Pacer.TempoType.FADE:
            case Pacer.TempoType.RELAX:
                activeIntensityEnemyMax = calmEnemyAmount;
                break;
            default:
                break;
        }
        scalingTimer += Time.deltaTime;
        mobSpawnTimer += Time.deltaTime;

        if (scalingTimer > timeBetweenScaleUp)
        {
            IncreaseThreatSizes();
            scalingTimer = 0;
        }
        if (mobSpawnTimer > timeBetweenMobs && BoidManager.allBoids.Count < intenseEnemyAmount &&
            (currentTempo == Pacer.TempoType.BUILDUP || currentTempo == Pacer.TempoType.SUSTAIN))
        {
            mobReady = true;
            mobSpawnTimer = 0;
        }
    }

    public void OnPlayerNodeChange(List<Node> oldActiveArea, List<Node> newActiveArea, Vector3 playerPos)
    {
        List<Node> newActiveAreaDiff = new List<Node>(newActiveArea);
        List<Node> oldActiveAreaDiff = new List<Node>(oldActiveArea);
        RemoveCommonNodes(ref oldActiveAreaDiff, ref newActiveAreaDiff);
        PurgeEnemies(oldActiveAreaDiff, playerPos);
        SpawnWanderers(newActiveAreaDiff, newActiveArea[0].percentToEnd);
    }

    private void SpawnWanderers(List<Node> newActiveAreaDiff, float playerNodeDistToEnd)
    {
        if (BoidManager.allBoids.Count >= activeIntensityEnemyMax)
        {
            return;
        }

        for (int i = newActiveAreaDiff.Count - 1; i >= 0; i--)
        {

            if (newActiveAreaDiff[i].percentToEnd < playerNodeDistToEnd)
            {
                newActiveAreaDiff.RemoveAt(i);
            }

        }

        for (int i = 0; i < newActiveAreaDiff.Count * stragelerGroupSize; i++)
        {
            GameObject enemy = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
            if (enemy.TryGetComponent<SwarmerBT>(out SwarmerBT swarmerBT))
            {
                for (int j = 0; j < nrSwarmerPerSpawn; j++)
                {
                    newActiveAreaDiff[i % newActiveAreaDiff.Count].spawner.Spawn(enemy);
                }
            }
            else
            {
                newActiveAreaDiff[i % newActiveAreaDiff.Count].spawner.Spawn(enemy);
            }
            if (BoidManager.allBoids.Count > activeIntensityEnemyMax)
                break;
        }
    }

    private void PurgeEnemies(List<Node> nodesOutOfRange, Vector3 playerPos)
    {
        for (int j = BoidManager.allBoids.Count - 1; j >= 0; j--)
        {
            if (Vector3.Distance(BoidManager.allBoids[j].transform.position, playerPos) > purgeDistance)
            {
                Destroy(BoidManager.allBoids[j].gameObject);
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
        if (BoidManager.allBoids.Count >= activeIntensityEnemyMax + mobSize)
        {
            return;
        }
        mobReady = false;
        mobSpawnTimer = 0;
        timeBetweenMobs = UnityEngine.Random.Range(mobMinSpawnInterval, mobMaxSpawnInterval);
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
        for (int i = 0; i < nrNodesAway; i++)
        {
            if (path.nodes.Count > 0)
            {
                spawnNode = path.nodes.Pop();
            }
        }
        Debug.Log("Spawns mob on node " + spawnNode.spawner.name);
        for (int i = 0; i < mobSize; i++)
        {
            if (BoidManager.allBoids.Count > activeIntensityEnemyMax + mobSize)
            {
                break;
            }
            GameObject enemy = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
            if (enemy.TryGetComponent<SwarmerBT>(out SwarmerBT swarmerBT))
            {
                for (int s = 0; s < nrSwarmerPerSpawn; s++)
                {
                    spawnNode.spawner.Spawn(enemy);
                }
            }
            else
            {
                spawnNode.spawner.Spawn(enemy);
            }
        }
    }
}

