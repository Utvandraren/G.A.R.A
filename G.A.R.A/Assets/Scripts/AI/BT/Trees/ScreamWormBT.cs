using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamWormBT : BehaviorTree
{
    protected override void Start()
    {
        base.Start();
        MakeTree();
    }

    protected override void MakeTree()
    {
        Task[] chargeArr = {  new TargetPlayer(), new TurnToward(), new TurnToward(), new MoveTowards() };
        Task chargeSeq = new Sequence(chargeArr);
        Task[] root = {chargeSeq};
        this.root = new Selector(root);
    }
}
