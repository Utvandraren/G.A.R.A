using UnityEngine;
public class CheckInMinWeaponRange : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        if (Vector3.Distance(behaviorTree.BlackBoard.transform.position,
            behaviorTree.BlackBoard.target) < behaviorTree.BlackBoard.weaponMinRange)
        {
            return Status.success;
        }
        else
        {
            return Status.failed;
        }
    }
}

