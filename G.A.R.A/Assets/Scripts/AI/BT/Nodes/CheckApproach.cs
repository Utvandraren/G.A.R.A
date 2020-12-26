/// <summary>
/// Not to be implemented
/// Used as a base for the actual condtion nodes
/// Copy this class to the Nodes folder and give descriptive name
/// </summary>
public class CheckApproach : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        if (behaviorTree.BlackBoard.readyForApproach)
            return Status.success;
        else
            return Status.failed;
    }
}

