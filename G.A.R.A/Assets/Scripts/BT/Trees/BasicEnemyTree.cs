using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyTree : BehaviorTree
{
    BOID movementSystem;
    //Weapon weapon;
    protected override void Start()
    {
        base.Start();
        movementSystem = GetComponent<BOID>();
        LoadTree(this);
    }

    private void LoadTree(BasicEnemyTree basicEnemyTree)
    {
        
        throw new NotImplementedException();
    }
}
