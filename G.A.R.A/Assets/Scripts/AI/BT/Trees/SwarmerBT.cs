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
        Task[] CheckInMin = { new CheckInMinWeaponRange(), new MoveAway() };
        Task[] checkOutMax = { new Inverter( new CheckInMaxWeaponRange()), new MoveTowards()};
        Task checkMin = new Sequence(CheckInMin);
        Task checkMax = new Sequence(checkOutMax);
        Task[] DetMove = { checkMin,checkMax };
        Task movement = new Selector(DetMove);
        Task[] Targeting = { new TargetPlayer(), movement};
        Task targeting = new Sequence(Targeting);
        Task[] Root = { targeting, new Wander() };
        root = new Selector(Root);
    }
}
