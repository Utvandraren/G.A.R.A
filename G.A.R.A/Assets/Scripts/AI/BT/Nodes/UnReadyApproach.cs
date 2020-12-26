public class UnReadyApproach : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
            behaviorTree.BlackBoard.readyForApproach = false;
            return Status.success;
    }
}

