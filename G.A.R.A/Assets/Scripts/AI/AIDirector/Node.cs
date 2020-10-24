using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]
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
    public RoomType type;
    public Vector3 roomCenter;
    [HideInInspector] public float percentToEnd;
}

