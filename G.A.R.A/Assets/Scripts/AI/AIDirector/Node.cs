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
        NORMAL,
        START,
        STARTEXTEND,
        END,
    }
    public RoomType type = RoomType.NORMAL;
    //public Vector3 roomCenter;
    [HideInInspector] public float percentToEnd;
    public Spawner spawner;
}

