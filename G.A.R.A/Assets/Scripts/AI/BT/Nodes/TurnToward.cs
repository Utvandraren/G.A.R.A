public class TurnToward : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        behaviorTree.boidSystem.TurnGradual(behaviorTree.BlackBoard.target);
        return Status.success;
    }
}

