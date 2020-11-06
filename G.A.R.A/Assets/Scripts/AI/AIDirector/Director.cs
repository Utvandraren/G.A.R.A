using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField] private LevelGraph graph;
    PlayerReader playerReader;
    Pacer pacer;

    List<Edge.DoorType> doorTypes = new List<Edge.DoorType>();

    // Start is called before the first frame update
    void Start()
    {
        playerReader = GetComponent<PlayerReader>();
        pacer = GetComponent<Pacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pacer.started)
        {
            if (graph.FindStart() != graph.FindPlayerNode(playerReader.player.transform.position))
                pacer.started = true;

        }
    }

    void GivePacerPlayerData()
    {
        pacer.GivePlayerReader(playerReader);
    }

    void CheckAmmoRequirements()
    {
        Path path = graph.FindShortestPathToGoal(graph.playerNode);
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
