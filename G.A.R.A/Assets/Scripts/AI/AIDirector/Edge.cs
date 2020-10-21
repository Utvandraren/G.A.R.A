using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]
public class Edge
{
    public enum DoorType
    {
        NONE,
        INTERACT,
        DESTRUCTABLE,
        TOGGLEABLE
    }
    [SerializeField] private DoorType type;
    [SerializeField] private int from;
    [SerializeField] private int to;

    public Edge(int from, int to, DoorType type)
    {
        this.type = type;
        this.from = from;
        this.to = to;
    }
}

