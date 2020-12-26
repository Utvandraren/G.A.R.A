using UnityEngine;

public class MoveTowards : Task
{
    /// <summary>
    /// Moves towards the target
    /// </summary>
    /// <param name="behaviorTree"></param>
    /// <returns></returns>
    public override Status Tick(BehaviorTree behaviorTree)
    {
        Vector3 relativeTarget = behaviorTree.BlackBoard.target - behaviorTree.transform.position;
        behaviorTree.boidSystem.UpdateMovement(relativeTarget.normalized);
        return Status.success;
    }
}

