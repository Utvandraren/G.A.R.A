using UnityEngine;
public class CheckInMaxWeaponRange : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        if (Vector3.Distance(behaviorTree.BlackBoard.transform.position,
            behaviorTree.BlackBoard.target) < behaviorTree.BlackBoard.weaponMaxRange)
        {
            return Status.success;
        }
        else
        {
            return Status.failed;
        }
    }
}

