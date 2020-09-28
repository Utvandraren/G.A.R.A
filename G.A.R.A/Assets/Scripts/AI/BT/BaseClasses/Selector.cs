/// <summary>
/// To be implemented and used
/// </summary>
class Selector :Task
{
    Task[] children;

    public Selector(Task[] tasks)
    {
        children = tasks;
    }

    public override Status Tick(BehaviorTree behaviorTree)
    {
        foreach (Task task in children)
        {
            if (task.Tick(behaviorTree) != Status.failed)
                return Status.success;
        }
        return Status.failed;
    }
}

