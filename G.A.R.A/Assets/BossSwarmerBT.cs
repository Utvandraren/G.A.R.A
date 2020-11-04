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
        //Task[] CheckInMin = { new CheckInMinWeaponRange(), new UnReadyApproach() };
        //Task[] checkOutMax = { new Inverter(new CheckInMaxWeaponRange()), new ReadyApproach() };
        //Task checkMin = new Sequence(CheckInMin);
        //Task checkMax = new UnconditionalPositive(new Sequence(checkOutMax));
        //Task[] determineApproach = { checkMin, checkMax };
        //Task aprCheck = new Selector(determineApproach);
        //Task[] fireOrMoveSelArr = { fireSeq, new MoveTowards() };
        //Task fireSel = new Selector(fireOrMoveSelArr);
        //Task[] aprSeqArr = { new CheckApproach(), fireSel };
        //Task aprSeq = new Sequence(aprSeqArr);
        //Task[] moveSelArr = { aprSeq };
        //Task moveSel = new Selector(moveSelArr);
        //Task[] fightSeqArr = { new TargetPlayer(), new CheckInDetectionRange(), aprCheck, moveSel };
        //Task fightSeq = new Sequence(fightSeqArr);

        Task[] fireSeqArr = { new CheckInMaxWeaponRange(), new TargetPlayer(), new TurnToward(), new Fire() };
        Task fireSeq = new Sequence(fireSeqArr);
        Task[] rootArr = { fireSeq, new Wander() };
        root = new Selector(rootArr);
    }
}
