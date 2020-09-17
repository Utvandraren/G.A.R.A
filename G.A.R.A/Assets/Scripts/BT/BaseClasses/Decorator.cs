/// <summary>
/// Not to be implemented
/// Used as a base for the actual Decorator nodes
/// Copy this class to the Nodes folder and give descriptive name
/// </summary>
class Decorator : Task
{
    Task child;
    public override Status Tick()
    {
        return child.Tick();
    }
}

