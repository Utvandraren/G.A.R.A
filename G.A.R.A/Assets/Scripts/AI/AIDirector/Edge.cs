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
    public int one { get; private set; }
    public int other { get; private set; }

    public Edge(int one, int other, DoorType type)
    {
        this.type = type;
        this.one = one;
        this.other = other;
    }
}

