using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Edge
{
    public enum DoorType
    {
        NONE,
        INTERACT,
        DESTRUCTABLE,
        TOGGLEABLE
    }
    DoorType type;
    Node from;
    Node to;

    public Edge(Node from, Node to, DoorType type)
    {
        this.type = type;
        this.from = from;
        this.to = to;
    }
    
    public Edge(Node from, Node to)
    {
        this.type = DoorType.NONE;
        this.from = from;
        this.to = to;
    }
}

