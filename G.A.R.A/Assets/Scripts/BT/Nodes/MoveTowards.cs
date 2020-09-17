public class MoveTowards : Task
{
    public override Status Tick(BehaviorTree behaviorTree)
    {
        bool succeeded = true;
        bool failed = false;
        bool HandleStub()
        {
            //Get target (condition)
            //get dir to target (condition)
            //Move in dir
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

