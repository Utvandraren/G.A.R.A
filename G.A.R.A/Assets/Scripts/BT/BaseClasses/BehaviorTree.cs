using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class BehaviorTree : MonoBehaviour
{
    Task root;
    internal BOID boidSystem;
    public BlackBoard BlackBoard { get; private set; }

    protected virtual void Start()
    {
        BlackBoard = GetComponent<BlackBoard>();
        boidSystem = GetComponent<BOID>();
        root = new Wander();
    }

    private void Update()
    {
        root.Tick(this);
    }
}

