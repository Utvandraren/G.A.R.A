/// <summary>
/// Not to be implemented
/// Used as a base for the actual action nodes
/// Copy this class to the Nodes folder and give descriptive name
/// </summary>
public class MoveTowards : Task
{
    public override Status Tick()
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

