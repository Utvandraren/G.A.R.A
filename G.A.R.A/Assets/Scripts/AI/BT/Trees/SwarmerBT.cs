using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerBT : BehaviorTree
{
    protected override void Start()
    {
        base.Start();
        MakeTree();
    }

    protected override void MakeTree()
    {
        Task[] reset = { new TargetPlayer(), new CheckInWeaponRange(), new MoveAway() };
        Task[] fireAproach = { new CheckWillToFight(), new TargetPlayer(), new CheckInWeaponRange(), new CheckLineOfSight(), new MoveTowards(), new Fire() };
        Task[] getInRage = { new CheckWillToFight(), new TargetPlayer(), new CheckInRange(), new MoveTowards() };
        Task fireSeq = new Sequence(fireAproach);
        Task goTo = new Sequence(getInRage);
        Task[] root = { fireSeq, goTo, new Wander() };
        this.root = new Selector(root);
    }
}
