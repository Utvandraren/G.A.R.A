public class ReadyApproach : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        bool succeeded = true;
        bool failed = false;
        bool HandleStub()
        {
            behaviorTree.BlackBoard.readyForApproach = true;
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

