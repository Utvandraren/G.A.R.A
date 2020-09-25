using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactions : MonoBehaviour
{
    public ActivationEnum active;

    public bool resetable; //If reset should be possible
    public bool isActive;

    public virtual void SwitchActiveState()
    {

    }

    public virtual void DestroyTheObject()
    {
    }
}