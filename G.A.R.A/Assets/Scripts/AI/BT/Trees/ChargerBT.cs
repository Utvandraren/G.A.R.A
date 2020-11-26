using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBT : BehaviorTree
{
    protected override void Start()
    {
        base.Start();
        MakeTree();
    }

    protected override void MakeTree()
    {
        Task[] fire = { new CheckWillToFight(), new TargetPlayer(), new CheckInMinWeaponRange(), new CheckLineOfSight(), new TurnToward(), new Fire() };
        Task[] chargeArr = { new CheckWillToFight(), new TargetPlayer(), new CheckInMaxWeaponRange(), new MoveTowards() };
        Task[] getInRage = { new CheckWillToFight(), new TargetPlayer(), new CheckInDetectionRange(), new MoveTowards() };
        Task fireSeq = new Sequence(fire);
        Task chargeSeq = new Sequence(chargeArr);
        Task goTo = new Sequence(getInRage);
        Task[] root = { fireSeq, chargeSeq, goTo, new Wander() };
        this.root = new Selector(root);
    }
}
