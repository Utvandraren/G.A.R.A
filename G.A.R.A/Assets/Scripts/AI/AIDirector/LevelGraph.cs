using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGraph
{
    List<Node> nodes;
    List<Edge> edges;

    public void AddNode()
    {
        Node node = new Node();
        nodes.Add(node);
    }
}
