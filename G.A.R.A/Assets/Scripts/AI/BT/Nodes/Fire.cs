public class Fire : Task
{
    /// <summary>
    /// Tries to fire weapons
    /// </summary>
    /// <param name="behaviorTree"></param>
    /// <returns>Success Status</returns>
    public override Status Tick(BehaviorTree behaviorTree)
    {
        behaviorTree.weapon.TryShoot();
        return Status.success;
    }
}

