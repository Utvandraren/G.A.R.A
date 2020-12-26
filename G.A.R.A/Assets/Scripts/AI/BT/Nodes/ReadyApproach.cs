public class ReadyApproach : Task
{
    /// <summary>
    /// Readies the entity for approach
    /// </summary>
    /// <param name="behaviorTree"></param>
    /// <returns></returns>
    public override Status Tick(BehaviorTree behaviorTree)
    {
        behaviorTree.BlackBoard.readyForApproach = true;
        return Status.success;
    }
}

