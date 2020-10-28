using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Graph", menuName = "ScriptableObjects/LevelGraph", order = 1)]
public class LevelGraph : ScriptableObject
{
    [SerializeField] private Node[] nodes;
    [SerializeField] private Tuple<int, int, Edge.DoorType>[] edgeData;
    private List<Edge>[] edges;
    public void Initialize()
    {
        edges = new List<Edge>[nodes.Length];
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i] = new List<Edge>();
        }
        CreateEdges();
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
}
