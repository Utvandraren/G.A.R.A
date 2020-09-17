/// <summary>
/// Not to be implemented
/// Used as a base for the actual condtion nodes
/// Copy this class to the Nodes folder and give descriptive name
/// </summary>
public class CheckLineOfSight : Task
{
    bool someCondition;
    public override Status Tick()
    {
        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

