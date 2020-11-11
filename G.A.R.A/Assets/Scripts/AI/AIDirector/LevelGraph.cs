using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelGraph : MonoBehaviour
{
    [SerializeField] public Node[] nodes;
    [SerializeField] private EdgeDescription[] edgeData;
    private List<Edge>[] edges;
    [HideInInspector] public int playerNode = -1;

    public event EventHandler ChangedNode;

    public void Initialize()
    {
        edges = new List<Edge>[nodes.Length];
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i] = new List<Edge>();
        }
        CreateEdges();
        playerNode = FindStart();
    }
    private void CreateEdges()
    {
        foreach (var tuple in edgeData)
        {
            edges[tuple.Item1].Add(new Edge(tuple.Item2, tuple.Item3));
            edges[tuple.Item2].Add(new Edge(tuple.Item1, tuple.Item3));
        }

    }
    public int FindEnd()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].type == Node.RoomType.END)
                return i;
        }
        throw new NullReferenceException("No \"End\" node in graph");
    }
    public int FindStart()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].type == Node.RoomType.START)
                return i;
        }
        throw new NullReferenceException("No \"Start\" node in graph");
    }

    public int FindPlayerNode(Vector3 playerPos)
    {
        bool movedNode = false;
        float savedNodeDistance = Vector3.Distance(nodes[playerNode].spawner.transform.position, playerPos);
        while (true)
        {
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
                savedNodeDistance = closestNeighboringNodeDistance;
                movedNode = true;
            }
            else
                break;
        }
        if (movedNode)
            ChangedNode?.Invoke(this, new EventArgs());
        return playerNode;
    }

    public void SetNodeGradient()
    {
        bool[] visited = new bool[nodes.Length];
        Queue<int> nodeQue = new Queue<int>();
        nodeQue.Enqueue(FindEnd());
        int treeDepth = 0;
        int newDepthNode = nodeQue.Peek();
        while (nodeQue.Count > 0)
        {
            int activeNode = nodeQue.Dequeue();
            nodes[activeNode].percentToEnd = treeDepth;
            visited[activeNode] = true;
            if (activeNode == newDepthNode)
            {
                treeDepth++;
            }
            foreach (Edge edge in edges[activeNode])
            {
                int toIndex = edge.to;
                if (!visited[toIndex])
                {
                    nodeQue.Enqueue(toIndex);
                    if (activeNode == newDepthNode)
                    {
                        newDepthNode = toIndex;
                    }
                }
            }
        }
        foreach (Node node in nodes)
        {
            node.percentToEnd = (float)((treeDepth - node.percentToEnd) / treeDepth);
        }
    }

    public Node[] GetFrontNodes(int index)
    {
        List<Node> frontNodes = new List<Node>();
        foreach (Node node in nodes)
        {
            if (node.percentToEnd > nodes[index].percentToEnd)
                frontNodes.Add(node);
        }
        return frontNodes.ToArray();
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
            throw new ArgumentOutOfRangeException("tried using node \"-1\"");
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
        return Radiate(playerNode, 2);
    }

    public List<Node> Radiate(int origo, int depth)
    {
        List<Node> radialNodes = new List<Node>();
        bool[] visited = new bool[nodes.Length];
        Queue<int> nodeQue = new Queue<int>();

        nodeQue.Enqueue(origo);
        int newDepthNode = nodeQue.Peek();
        int currentDepth = 0;
        while (nodeQue.Count > 0)
        {
            int activeNode = nodeQue.Dequeue();
            radialNodes.Add(nodes[activeNode]);
            visited[activeNode] = true;
            if (activeNode == newDepthNode)
            {
                currentDepth++;
                if (currentDepth > depth)
                    break;
            }
            foreach (Edge edge in edges[activeNode])
            {
                int toIndex = edge.to;
                if (!visited[toIndex])
                {
                    nodeQue.Enqueue(toIndex);
                    if (activeNode == newDepthNode)
                    {
                        newDepthNode = toIndex;
                    }
                }
            }
        }
        return radialNodes;
    }
}
