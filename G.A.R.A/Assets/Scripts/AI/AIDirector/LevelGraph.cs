using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Graph", menuName = "ScriptableObjects/LevelGraph", order = 1)]
public class LevelGraph : ScriptableObject
{
    [SerializeField] private Node[] nodes;
    [SerializeField] private Edge[] edges;
    public Node FindEnd()
    {
        foreach (Node node in nodes)
        {
            if (node.type == Node.RoomType.END)
                return node;
        }
        throw new NullReferenceException();
    }
    public void SetNodeGradient()
    {
        Queue<Node> nodeQue;
        Node end = FindEnd();
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
