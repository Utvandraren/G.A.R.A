using UnityEngine;
public class CheckInWeaponRange : Task
{
    bool someCondition;

    public override Status Tick(BehaviorTree behaviorTree)
    {
        someCondition = false;
        if (Vector3.Distance(behaviorTree.BlackBoard.transform.position,
            behaviorTree.BlackBoard.target) < behaviorTree.BlackBoard.weaponRange)
            someCondition = true;
        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

