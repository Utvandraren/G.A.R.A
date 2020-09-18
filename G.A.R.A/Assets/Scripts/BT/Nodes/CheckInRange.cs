using UnityEngine;
public class CheckInRange : Task
{
    bool someCondition;
    public override Status Tick(BehaviorTree behaviorTree)
    {
        if (Vector3.Distance(behaviorTree.BlackBoard.transform.position,behaviorTree.BlackBoard.target) < behaviorTree.BlackBoard.detectionRange)
            someCondition = true;
        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

