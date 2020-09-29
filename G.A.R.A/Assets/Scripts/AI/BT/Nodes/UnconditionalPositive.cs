class UnconditionalPositive : Task
{
    Task child;
    public UnconditionalPositive(Task task)
    {
        child = task;
    }
    public override Status Tick(BehaviorTree behaviorTree)
    {
        child.Tick(behaviorTree);
        return Status.success;
    }
}

