using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    private LevelGraph graph;
    private SpawnManager spawnManager;
    private PlayerReader playerReader;
    private Pacer pacer;
    private Path shortestPath;
    private List<Node> activeArea;

    private List<Edge.DoorType> doorTypes = new List<Edge.DoorType>();

    private float timeBetweenPings = 0.1f;
    private float pingTimer = 0f;

    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        graph = GetComponent<LevelGraph>();
        pacer = GetComponent<Pacer>();
        spawnManager = GetComponent<SpawnManager>();
        playerReader = GetComponent<PlayerReader>();
        activeArea = new List<Node>();

        GivePacerPlayerData();
        graph.ChangedNode += Graph_ChangedNode;
        graph.Initialize();
        shortestPath = graph.FindShortestPathToGoal(graph.playerNode);
        activeArea = graph.FindActiveArea();
        playerReader.playerStats.tookDamage += PlayerStats_tookDamage;
    }

    private void PlayerStats_tookDamage(object player, TakeDamageEventArgs eventArgsDamage)
    {
        int damage = eventArgsDamage.damage;
        float damagePercent = (float)damage / playerReader.playerStats.health;
        pacer.IncreasePanicOnDamageTaken(damagePercent);
    }

    /// <summary>
    /// Activates functions when the player moves to a new node in the graph
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Graph_ChangedNode(object sender, System.EventArgs e)
    {
        if (!(graph.nodes[graph.playerNode].type == Node.RoomType.START || graph.nodes[graph.playerNode].type == Node.RoomType.STARTEXTEND))
        {
            ActivateDirector();
        }
        if (shortestPath.nodes.Count != 0)
        {

            if (shortestPath.nodes.Pop() != graph.nodes[graph.playerNode])
            {
                shortestPath = graph.FindShortestPathToGoal(graph.playerNode);
            }
            else
            {
                shortestPath.edges.Pop();
            }
        }
        else
        {
            shortestPath = graph.FindShortestPathToGoal(graph.playerNode);
        }
        //CheckAmmoRequirements();

        List<Node> newActiveArea = graph.FindActiveArea();
        if (active)
        {
            spawnManager.OnPlayerNodeChange(activeArea, newActiveArea, playerReader.player.transform.position);
        }
        activeArea = newActiveArea;
    }

    private void ActivateDirector()
    {
        active = true;
        pacer.Activate();
        spawnManager.Activate();
    }

    internal void EnemyIsDestroyed(Vector3 position)
    {
        float distance = Vector2.Distance(position, playerReader.player.transform.position);
        pacer.IncreasePanicOnKill(distance);
    }

    void Update()
    {
        pingTimer += Time.deltaTime;
        if (pingTimer > timeBetweenPings)
        {
            pingTimer = 0;
            graph.FindPlayerNode(playerReader.player.transform.position);
        }
        spawnManager.currentTempo = pacer.currentTempo;
        if (spawnManager.mobReady)
        {
            spawnManager.SpawnMob(new Path(shortestPath));
        }
    }

    void GivePacerPlayerData()
    {
        pacer.StorePlayerReader(playerReader);
    }

    void CheckAmmoRequirements()
    {
        Path path = new Path(shortestPath);
        doorTypes.Clear();
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

    }
}
