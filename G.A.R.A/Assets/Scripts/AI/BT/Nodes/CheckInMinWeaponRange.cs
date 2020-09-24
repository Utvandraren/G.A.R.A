using UnityEngine;
public class CheckInMinWeaponRange : Task
{
    bool someCondition;

    public override Status Tick(BehaviorTree behaviorTree)
    {
        someCondition = false;
        if (Vector3.Distance(behaviorTree.BlackBoard.transform.position,
            behaviorTree.BlackBoard.target) < behaviorTree.BlackBoard.weaponMinRange)
            someCondition = true;
        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

