﻿public class TurnToward : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        bool succeeded = true;
        bool failed = false;
        bool HandleStub()
        {
            behaviorTree.engine.LookTowards(behaviorTree.BlackBoard.target);
            return true;
        }

        if (HandleStub() == succeeded)
            return Status.success;
        else if (HandleStub() == failed)
            return Status.failed;
        else
            return Status.running;
    }
}

