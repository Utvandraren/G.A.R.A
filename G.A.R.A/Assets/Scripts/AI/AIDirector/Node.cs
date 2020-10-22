using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]
public class Node
{
    public enum RoomType
    {
        START,
        TARGET,
        END,
        NO_SPAWN,
        SPAWN
    }
    [HideInInspector] public int index = -1;
    public RoomType type;
    public Vector3 roomCenter;
    public int[] edgeTo;
    [HideInInspector] public List<Edge> edges;
    [HideInInspector] public float percentToEnd;
    public Node()
    {
        edges = new List<Edge>();
    }

    internal void CreateEdge(int toNodeIndex, Edge.DoorType edgeType)
    {
        edges.Add(new Edge(toNodeIndex, edgeType));
    }
}

