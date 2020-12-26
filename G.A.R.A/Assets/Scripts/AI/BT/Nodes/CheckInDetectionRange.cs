﻿using UnityEngine;
public class CheckInDetectionRange : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        if (Vector3.Distance(behaviorTree.BlackBoard.transform.position, behaviorTree.BlackBoard.target) < behaviorTree.BlackBoard.detectionRange)
        {
            return Status.success;
        }
        else
        {
            return Status.failed;
        }
    }
}

