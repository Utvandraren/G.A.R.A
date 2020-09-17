using UnityEngine;
public class CheckLineOfSight : Task
{
    bool someCondition;
    public override Status Tick(BehaviorTree behaviorTree)
    {
        if (Physics.Raycast(behaviorTree.transform.position, behaviorTree.BlackBoard.target - behaviorTree.transform.position))
            someCondition = true;
        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

