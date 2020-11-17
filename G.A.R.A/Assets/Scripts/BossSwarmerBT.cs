using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwarmerBT : BehaviorTree
{
    protected override void Start()
    {
        base.Start();
        MakeTree();
    }

    protected override void MakeTree()
    {
        Task[] fire = { new TargetPlayer(), new CheckInMaxWeaponRange(), new CheckLineOfSight(), new TurnToward(), new Stop(), new Fire() };
        Task fireSeq = new Sequence(fire);
        Task[] root = { fireSeq, /*new Wander()*/ };
        this.root = new Selector(root);
    }
}
