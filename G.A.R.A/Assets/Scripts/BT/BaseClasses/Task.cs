/// <summary>
/// Base class of the entire BT system
/// </summary>
public abstract class Task
{
    public enum Status
    {
        failed, success, running
    }
    public Status taskStatus { get; }
    public abstract Status Tick(BehaviorTree behaviorTree);
}
