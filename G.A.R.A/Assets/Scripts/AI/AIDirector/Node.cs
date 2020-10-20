using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Node
{
    public enum RoomType
    {
        START,
        TARGET,
        END,
        NO_SPAWN,
        SPAWN
    }
    RoomType type;
    Transform roomBounds;
    public Node(RoomType type, Transform roomBounds)
    {
        this.type = type;
        this.roomBounds = roomBounds;
    }
}

