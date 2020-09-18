﻿class Inverter : Task
{
    Task child;
    public override Status Tick(BehaviorTree behaviorTree)
    {
        Status status = child.Tick(behaviorTree);
        if (status == Status.failed)
            return Status.success;
        else if (status == Status.success)
            return Status.failed;
        else
            return status;
    }
}

