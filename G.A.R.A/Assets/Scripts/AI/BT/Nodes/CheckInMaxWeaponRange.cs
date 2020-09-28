using UnityEngine;
public class CheckInMaxWeaponRange : Task
{
    bool someCondition;

    public override Status Tick(BehaviorTree behaviorTree)
    {
        someCondition = false;
        if (Vector3.Distance(behaviorTree.BlackBoard.transform.position,
            behaviorTree.BlackBoard.target) < behaviorTree.BlackBoard.weaponMaxRange)
            someCondition = true;
        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

