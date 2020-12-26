using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelGraph : MonoBehaviour
{
    [SerializeField] private int activeAreaDepth = 2;
    [SerializeField] public Node[] nodes;
    [SerializeField] private EdgeDescription[] edgeData;
    private List<Edge>[] edges;
    [HideInInspector] public int playerNode = -1;

    public event EventHandler ChangedNode;

    public void OnDrawGizmos()
    {
        foreach (EdgeDescription edge in edgeData)
        {
            Vector3 one = nodes[edge.Item1].spawner.transform.position;
            Vector3 other = nodes[edge.Item2].spawner.transform.position;
            Gizmos.DrawLine(one, other);
        }
    }
    public void Initialize()
    {
        edges = new List<Edge>[nodes.Length];
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i] = new List<Edge>();
        }
        CreateEdges();
        SetNodeGradient();
        playerNode = FindStart();
    }
    /// <summary>
    /// Creates edges from the developer inputed edgedata classes
    /// </summary>
    private void CreateEdges()
    {
        foreach (EdgeDescription desciption in edgeData)
        {
            edges[desciption.Item1].Add(new Edge(desciption.Item2, desciption.Item3));
            edges[desciption.Item2].Add(new Edge(desciption.Item1, desciption.Item3));
        }

    }
    public int FindEnd()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].type == Node.RoomType.END)
            {
                return i;
            }
        }
        throw new NullReferenceException("No \"End\" node in graph");
    }
    public int FindStart()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].type == Node.RoomType.START)
            {
                return i;
            }
        }
        throw new NullReferenceException("No \"Start\" node in graph");
    }

    public int FindPlayerNode(Vector3 playerPos)
    {
        float savedNodeDistance = Vector3.Distance(nodes[playerNode].spawner.transform.position, playerPos);
        float closestNeighboringNodeDistance = int.MaxValue;
        int closerNode = -1;
        foreach (Edge edge in edges[playerNode])
        {
            float CurrentNodeDistance = Vector3.Distance(nodes[edge.to].spawner.transform.position, playerPos);
            if (closestNeighboringNodeDistance > CurrentNodeDistance)
            {
                closerNode = edge.to;
                closestNeighboringNodeDistance = CurrentNodeDistance;
            }
        }
        if (closestNeighboringNodeDistance < savedNodeDistance)
        {
            playerNode = closerNode;
            Debug.Log("Changed to node " + playerNode);
            savedNodeDistance = closestNeighboringNodeDistance;
            ChangedNode?.Invoke(this, new EventArgs());
        }
        return playerNode;
    }
    public int[] CreateMinSpanTree(int from)
    {
        bool[] visited = new bool[nodes.Length];
        int[] minSpanningTree = new int[nodes.Length];
        Queue<int> nodeQue = new Queue<int>();
        nodeQue.Enqueue(from);
        while (nodeQue.Count > 0)
        {
            int activeNode = nodeQue.Dequeue();
            visited[activeNode] = true;
            foreach (Edge edge in edges[activeNode])
            {
                int toIndex = edge.to;
                if (!visited[toIndex])
                {
                    visited[toIndex] = true;
                    minSpanningTree[edge.to] = activeNode;
                    nodeQue.Enqueue(toIndex);
                }
            }
        }
        return minSpanningTree;
    }
    public Path FindShortestPathToGoal(int from)
    {
        if (from == -1)
            throw new ArgumentOutOfRangeException("Origin node was not specified (\"-1\")");
        Path path = new Path();
        int[] minSpanTree = CreateMinSpanTree(from);
        for (int nodeId = FindEnd(); nodeId != from; nodeId = minSpanTree[nodeId])
        {
            path.AddNode(nodes[nodeId]);
            for (int i = 0; i < edges[nodeId].Count; i++)
            {
                if (edges[nodeId][i].to == minSpanTree[nodeId])
                {
                    path.AddEdge(edges[nodeId][i]);
                    break;
                }
            }
        }
        return path;
    }

    public List<Node> FindActiveArea()
    {
        List<Node> activeArea = new List<Node>();
        bool[] enqueued = new bool[nodes.Length];
        Queue<int> nodeQue = new Queue<int>();
        nodeQue.Enqueue(playerNode);
        enqueued[nodeQue.Peek()] = true;
        nodeQue.Enqueue(-1);
        int treeDepth = 0;
        while (nodeQue.Count > 0)
        {
            bool newDepth = false;
            int activeNode = nodeQue.Dequeue();
            if (activeNode == -1)
            {
                treeDepth++;
                if(treeDepth > activeAreaDepth)
                {
                    break;
                }
                continue;
            }
            if (nodeQue.Peek() == -1)
            {
                newDepth = true;
            }

            activeArea.Add(nodes[activeNode]);

            foreach (Edge edge in edges[activeNode])
            {
                int toIndex = edge.to;
                if (!enqueued[toIndex])
                {
                    nodeQue.Enqueue(toIndex);
                    enqueued[toIndex] = true;
                }
            }
            if (newDepth)
            {
                nodeQue.Enqueue(-1);
            }
        }
        return activeArea;
    }
   /// <summary>
   /// Functions by marking changes in depth with a "-1" value in the queue
   /// </summary>
    public void SetNodeGradient()
    {
        bool[] enqueued = new bool[nodes.Length];
        Queue<int> nodeQue = new Queue<int>();
        nodeQue.Enqueue(FindEnd());
        enqueued[nodeQue.Peek()] = true;
        nodeQue.Enqueue(-1);
        int treeDepth = 0;
        while (nodeQue.Count > 0)
        {
            bool newDepth = false;
            int activeNode = nodeQue.Dequeue();
            if(activeNode == -1)
            {
                treeDepth++;
                continue;
            }
            nodes[activeNode].percentToEnd = treeDepth;
            if(nodeQue.Peek() == -1)
            {
                newDepth = true;
            }
            foreach (Edge edge in edges[activeNode])
            {
                int toIndex = edge.to;
                if (!enqueued[toIndex])
                {
                    nodeQue.Enqueue(toIndex);
                    enqueued[toIndex] = true;
                }
            }
            if (newDepth)
            {
                nodeQue.Enqueue(-1);
            }
        }
        foreach (Node node in nodes)
        {
            node.percentToEnd = ((float)treeDepth - (float)node.percentToEnd) / (float)treeDepth;
        }
    }
}
