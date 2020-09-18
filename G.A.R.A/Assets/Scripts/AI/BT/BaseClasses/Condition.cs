/// <summary>
/// Not to be implemented
/// Used as a base for the actual condtion nodes
/// Copy this class to the Nodes folder and give descriptive name
/// </summary>
public class Condition : Task
{
    bool someCondition = false;
    public override Status Tick(BehaviorTree behaviorTree)
    {
        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

