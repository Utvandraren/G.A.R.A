using UnityEngine;

public class MoveTowards : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        bool succeeded = true;
        bool failed = false;
        bool HandleStub()
        {
            Vector3 relativeTarget = behaviorTree.BlackBoard.target - behaviorTree.transform.position;
            behaviorTree.boidSystem.UpdateMovement(relativeTarget.normalized);
            return true;
        }

        if (HandleStub() == succeeded)
            return Status.success;
        else if (HandleStub() == failed)
            return Status.failed;
        else
            return Status.running;
    }
}

