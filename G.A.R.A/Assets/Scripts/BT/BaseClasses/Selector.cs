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

    public override Status Tick()
    {
        foreach (Task task in children)
        {
            if (task.Tick() != Status.failed)
                return task.taskStatus;
        }
        return Status.failed;
    }
}

