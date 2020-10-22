using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Graph", menuName = "ScriptableObjects/SimpleGraph", order = 1)]
public class SimpleGraph : ScriptableObject
{
    [SerializeField] Node [] nodes;

    [Serializable]
    class Node
    {
        [SerializeField] int index;
        [SerializeField] int[] adjacencies;
    }
}
