﻿/// <summary>
/// To be implemented and used
/// Returns sucess only if all childern succeeds
/// Used to implement a sequence of dependent tasks
/// </summary>
class Sequence : Task
{
    Task[] children;

    public Sequence(Task[] tasks)
    {
        children = tasks;
    }
    public override Status Tick(BehaviorTree behaviorTree)
    {
        foreach (Task task in children)
        {
            if (task.Tick(behaviorTree) == Status.failed)
                return Status.failed;
        }
        return Status.success;
    }
}

