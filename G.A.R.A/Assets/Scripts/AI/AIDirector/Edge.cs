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
    public DoorType type { get; private set; }
    public int to { get; private set; }

    public Edge(int to, DoorType type)
    {
        this.type = type;
        this.to = to;
    }
}

