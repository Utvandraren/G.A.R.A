using UnityEngine;

public class Wander : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        behaviorTree.boidSystem.UpdateMovement(Vector3.zero);
        return Status.success;
    }
}

