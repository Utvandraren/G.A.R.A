using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class BehaviorTree : MonoBehaviour
{
    protected Task root;
    internal BOID boidSystem;
    internal Weapon weapon;
    internal AIMoveEngine engine;
    public BlackBoard BlackBoard { get; private set; }

    protected virtual void Start()
    {
        engine = GetComponent<AIMoveEngine>();
        BlackBoard = GetComponent<BlackBoard>();
        boidSystem = GetComponent<BOID>();
        weapon = GetComponentInChildren<Weapon>();
        MakeTree();
    }

    protected virtual void MakeTree() { root = new Wander(); }

    private void Update()
    {
        root.Tick(this);
    }
}

