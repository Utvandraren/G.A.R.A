using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyTree : BehaviorTree
{
    protected override void Start()
    {
        base.Start();
        MakeTree();
    }

    protected override void MakeTree()
    {
        Task[] fire = {new CheckWillToFight(), new CheckInWeaponRange(), new CheckLineOfSight(), new Fire() };
        Task[] getInRage = { new CheckWillToFight(), new CheckInRange(), new MoveTowards() };
        Task fireSeq = new Sequence(fire);
        Task goTo = new Sequence(getInRage);
        Task[] root = { fireSeq, goTo, new Wander() };
        this.root = new Selector(root);
    }
}
