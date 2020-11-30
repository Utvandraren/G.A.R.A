using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : Task
{
    bool isActivated = false;
    float timeToWait = 2f;
    float currentTime = 0f;

    public override Status Tick(BehaviorTree behaviorTree)
    {

        if(isActivated)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                isActivated = false;
                return Status.success;
            }
            else
            {
                return Status.failed;
            }
        }
        else
        {
            isActivated = true;
            currentTime = timeToWait;
            return Status.failed;
        }

    }
}
