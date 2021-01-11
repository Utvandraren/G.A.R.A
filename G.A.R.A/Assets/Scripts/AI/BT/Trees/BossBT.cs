using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBT : BehaviorTree
{
    protected override void Start()
    {
        base.Start();
        MakeTree();
    }

    protected override void MakeTree()
    {
        Task[] fire = { new TargetPlayer(), new CheckInMaxWeaponRange()/*, new CheckLineOfSight()*/, new Stop(),  new Fire() };
        Task[] getInRange = { new TargetPlayer(), new CheckInDetectionRange(), new MoveTowards() };
        Task fireSeq = new Sequence(fire);
        Task goTo = new Sequence(getInRange);
        Task[] root = { fireSeq,  goTo, new Wander() };
        this.root = new Selector(root);
    }
}
