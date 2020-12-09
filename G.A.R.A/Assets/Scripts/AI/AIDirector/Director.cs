using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    private LevelGraph graph;
    private SpawnManager spawnManager;
    PlayerReader playerReader;
    Pacer pacer;
    Path shortestPath;
    List<Node> activeArea;

    List<Edge.DoorType> doorTypes = new List<Edge.DoorType>();

    float nodeTime;

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
        float damagePercent = (float)damage / playerReader.playerStats.startingHealth;
        pacer.IncreasePanic(damagePercent);
    }

    /// <summary>
    /// Activates functions when the player moves to a new node in the graph
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Graph_ChangedNode(object sender, System.EventArgs e)
    {
        if (!spawnManager.started && graph.nodes[graph.playerNode].type == Node.RoomType.START)
            return;
        spawnManager.started = true;
        pacer.StartedLevel();
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
        CheckAmmoRequirements();

        List<Node> newActiveArea = graph.FindActiveArea();
        spawnManager.OnPlayerNodeChange(graph.nodes[graph.playerNode], activeArea, newActiveArea, playerReader.player.transform.position);
        activeArea = newActiveArea;
    }

    private void Update()
    {
        nodeTime += Time.deltaTime;
    }

    void FixedUpdate()
    {
        graph.FindPlayerNode(playerReader.player.transform.position);
        spawnManager.currentTempo = pacer.currentTempo;
        if (spawnManager.mobReady)
        {
            spawnManager.SpawnMob(graph.FindShortestPathToGoal(graph.playerNode));
            spawnManager.IncreaseThreatSizes();
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
