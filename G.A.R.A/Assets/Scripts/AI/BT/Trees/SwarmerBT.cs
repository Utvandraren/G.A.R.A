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
        Task[] CheckInMin = { new CheckInMinWeaponRange(), new UnReadyApproach() };
        Task[] checkOutMax = { new Inverter(new CheckInMaxWeaponRange()), new ReadyApproach() };
        Task checkMin = new Sequence(CheckInMin);
        Task checkMax = new UnconditionalPositive(new Sequence(checkOutMax));
        Task[] determineApproach = { checkMin, checkMax };
        Task aprCheck = new Selector(determineApproach);
        Task[] fireSeqArr = { new CheckInMaxWeaponRange(), new MoveTowards(), new Fire() };
        Task fireSeq = new Sequence(fireSeqArr);
        Task[] fireOrMoveSelArr = { fireSeq, new MoveTowards() };
        Task fireSel = new Selector(fireOrMoveSelArr);
        Task[] aprSeqArr = { new CheckApproach(), fireSel };
        Task aprSeq = new Sequence(aprSeqArr);
        Task[] moveSelArr = { aprSeq, new MoveAway() };
        Task moveSel = new Selector(moveSelArr);
        Task[] fightSeqArr = { new TargetPlayer(), new CheckInDetectionRange(), new CheckLineOfSight(), aprCheck, moveSel };
        Task fightSeq = new Sequence(fightSeqArr);
        Task[] rootArr = { fightSeq, new Wander() };
        root = new Selector(rootArr);
    }
}
