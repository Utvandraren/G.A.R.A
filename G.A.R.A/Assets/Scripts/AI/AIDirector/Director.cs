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
    }
    /// <summary>
    /// Activates functions when the player moves to a new node in the graph
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Graph_ChangedNode(object sender, System.EventArgs e)
    {
        spawnManager.started = true;
        pacer.StartedLevel();
        if (shortestPath.nodes.Pop() != graph.nodes[graph.playerNode])
            graph.FindShortestPathToGoal(graph.playerNode);
        else
            shortestPath.edges.Pop();
        CheckAmmoRequirements();

        List<Node> newActiveArea = graph.FindActiveArea();
        spawnManager.OnPlayerNodeChange(graph.nodes[graph.playerNode], activeArea, newActiveArea);
        activeArea = newActiveArea;
    }

    // Update is called once per frame
    void Update()
    {
        graph.FindPlayerNode(playerReader.player.transform.position);
        spawnManager.currentTempo = pacer.currentTempo;
        if (spawnManager.mobReady)
        {
            spawnManager.SpawnMob(graph.nodes[graph.playerNode], activeArea, doorTypes);
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
