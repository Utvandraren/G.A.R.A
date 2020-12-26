class Repeat : Task
{
    Task child;
    int repetitions;
    public Repeat(Task task, int repetitions)
    {
        child = task;
        this.repetitions = repetitions;
    }
    public override Status Tick(BehaviorTree behaviorTree)
    {
        for (int i = 0; i < repetitions; i++)
        {
            if(child.Tick(behaviorTree) == Status.failed)
            {
                return Status.failed;
            }
        }
        return Status.success;
    }
}

