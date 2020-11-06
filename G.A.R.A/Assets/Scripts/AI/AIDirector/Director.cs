﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField] private LevelGraph graph;
    [SerializeField] private SpawnManager spawnManager;
    PlayerReader playerReader;
    Pacer pacer;
    Path shortestPath;
    List<Node> activeArea;

    List<Edge.DoorType> doorTypes = new List<Edge.DoorType>();

    // Start is called before the first frame update
    void Start()
    {
        activeArea = new List<Node>();
        playerReader = GetComponent<PlayerReader>();
        pacer = GetComponent<Pacer>();
        GivePacerPlayerData();
        graph.ChangedNode += Graph_ChangedNode;
        shortestPath = graph.FindShortestPathToGoal(graph.playerNode);
    }
    /// <summary>
    /// Activates functions when the player moves to a new node in the graph
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Graph_ChangedNode(object sender, System.EventArgs e)
    {
        pacer.StartedLevel();
        if (shortestPath.nodes.Pop() != graph.nodes[graph.playerNode])
            graph.FindShortestPathToGoal(graph.playerNode);
        else
            shortestPath.edges.Pop();
        CheckAmmoRequirements();
    }

    // Update is called once per frame
    void Update()
    {

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
