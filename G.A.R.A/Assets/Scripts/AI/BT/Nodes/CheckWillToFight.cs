/// <summary>
/// Not to be implemented
/// Used as a base for the actual condtion nodes
/// Copy this class to the Nodes folder and give descriptive name
/// </summary>
public class CheckWillToFight : Task
{
    bool someCondition = false;
    public override Status Tick(BehaviorTree behaviorTree)
    {
        someCondition = false;
        //TODO: if stats ok = true

        
        someCondition = true;

        if (someCondition == true)
            return Status.success;
        else
            return Status.failed;
    }
}

