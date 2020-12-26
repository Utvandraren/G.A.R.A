public class TargetPlayer : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        behaviorTree.BlackBoard.target = behaviorTree.BlackBoard.PlayerTransform.position;
        return Status.success;
    }
}

