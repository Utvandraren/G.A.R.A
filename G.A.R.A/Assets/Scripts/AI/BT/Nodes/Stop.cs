public class Stop : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        behaviorTree.boidSystem.Stop();
        return Status.success;
    }
}

