using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switches active state when invoked.
/// </summary>
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
