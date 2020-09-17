class Inverter : Task
{
    Task child;
    public override Status Tick()
    {
        Status status = child.Tick();
        if (status == Status.failed)
            return Status.success;
        else if (status == Status.success)
            return Status.failed;
        else
            return status;
    }
}

