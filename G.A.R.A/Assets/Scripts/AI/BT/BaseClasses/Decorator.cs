/// <summary>
/// Not to be implemented
/// Used as a base for the actual Decorator nodes
/// Copy this class to the Nodes folder and give descriptive name
/// Decorators modifies its child node
/// </summary>
class Decorator : Task
{
    Task child;
    public override Status Tick(BehaviorTree behaviorTree)
    {
        return child.Tick(behaviorTree);
    }
}

