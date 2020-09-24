using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteraction : MonoBehaviour
{
    public ActivationEnum active;

    public void SwitchActiveState()
    {
        if(active == ActivationEnum.Off)
        {
            active = ActivationEnum.On;
        }
        else
        {
            active = ActivationEnum.Off;
        }
    }
}
