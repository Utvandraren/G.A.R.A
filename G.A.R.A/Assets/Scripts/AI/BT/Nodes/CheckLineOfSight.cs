using UnityEngine;
public class CheckLineOfSight : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        Physics.Raycast(behaviorTree.transform.position, behaviorTree.BlackBoard.target - behaviorTree.transform.position, out RaycastHit hitInfo);
        if (hitInfo.collider.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
            return Status.success;
        else
            return Status.failed;
    }
}

