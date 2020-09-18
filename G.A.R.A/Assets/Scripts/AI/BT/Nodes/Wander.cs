using UnityEngine;

public class Wander : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        bool succeeded = true;
        bool failed = false;
        bool HandleStub()
        {
            behaviorTree.boidSystem.UpdateMovement(Vector3.zero);
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

