using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Graph", menuName = "ScriptableObjects/LevelGraph", order = 1)]
public class LevelGraph : ScriptableObject
{
    [SerializeField] private Node[] nodes;
    [SerializeField] private Tuple<int, int, Edge.DoorType>[] edgeData;
    private Edge[] edges;
    public void Initialize()
    {
        edges = CreateEdges();
    }
    private Edge[] CreateEdges()
    {
        List<Edge> tempEdges = new List<Edge>();
        foreach (var tuple in edgeData)
        {
            tempEdges.Add(new Edge(tuple.Item1, tuple.Item2, tuple.Item3));
        }
        return tempEdges.ToArray();
    }
    public Node FindEnd()
    {
        foreach (Node node in nodes)
        {
            if (node.type == Node.RoomType.END)
                return node;
        }
        throw new NullReferenceException("No \"End\" node in graph");
    }
    public void SetNodeGradient()
    {
        bool[] visited = new bool[nodes.Length];
        Queue<Node> nodeQue = new Queue<Node>();
        nodeQue.Enqueue(FindEnd());
        int treeDepth = 0;
        Node newDepthNode = nodeQue.Peek();
        while (nodeQue.Count > 0)
        {
            Node activeNode = nodeQue.Dequeue();
            activeNode.percentToEnd = treeDepth;
            visited[activeNode.index] = true;
            if (activeNode == newDepthNode)
            {
                treeDepth++;
            }
            foreach (Edge edge in activeNode.edges)
            {
                int toIndex = edge.to;
                if (!visited[toIndex])
                {
                    nodeQue.Enqueue(nodes[toIndex]);
                    if (activeNode == newDepthNode)
                    {
                        newDepthNode = nodes[toIndex];
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

    public Path FindShortestPath(int from, int to)
    {
        Path path = new Path();
        bool[] visited = new bool[nodes.Length];
        Queue<Node> nodeQue = new Queue<Node>();

        nodeQue.Enqueue(nodes[from]);
        while (nodeQue.Count > 0)
        {
            Node activeNode = nodeQue.Dequeue();
            visited[activeNode.index] = true;
            if (activeNode == nodes[to])
            {

            }
            foreach (Edge edge in activeNode.edges)
            {
                int toIndex = edge.to;
                if (!visited[toIndex])
                {
                    nodeQue.Enqueue(nodes[toIndex]);
                }
            }
        }
        throw new ArgumentException("No Path Found");
    }
    public Path FindShortestPathToGoal(int from)
    {
        throw new NotImplementedException();
    }
    public Path FindShortestPathWithoutSpecialAmmo(int from, int to)
    {
        throw new NotImplementedException();
    }
}
