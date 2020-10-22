using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Graph", menuName = "ScriptableObjects/LevelGraph", order = 1)]
public class LevelGraph : ScriptableObject
{
    [SerializeField] private Node[] nodes;
    [SerializeField] private Tuple<int,int,Edge.DoorType>[] edgeData;
    public void Initialize()
    {
        SetIndicies();
        CreateEdges();
    }
    private void SetIndicies()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].index = i;
        }
    }
    private void CreateEdges()
    {
        foreach (var currentEdge in edgeData)
        {
            nodes[currentEdge.Item1].CreateEdge(currentEdge.Item2, currentEdge.Item3);
        }
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

    public Node[] FindShortestPath(int from, int to)
    {
        throw new NotImplementedException();
    }
    public Node[] FindShortestPathToGoal(int from)
    {
        throw new NotImplementedException();
    }
    public Node[] FindShortestPathWithoutSpecialAmmo(int from, int to)
    {
        throw new NotImplementedException();
    }
}
