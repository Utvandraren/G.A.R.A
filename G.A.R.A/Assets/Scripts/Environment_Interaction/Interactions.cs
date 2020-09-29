using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public ActivationEnum active;

    public float rangeToOpenMovement;
    public bool isFullyMoved;
    public bool reverseMovementSecondInteract;

    public bool isFullyRotated;

    public float rotationAngle;


    private bool destroy; //If object should be destroyed
    private Vector3 originalScale;

    private float noInteractionTimer;
    private float noInteractionCooldown;

    private bool inCooldown;

    public void Start()
    {
        destroy = false;
        inCooldown = false;
        noInteractionCooldown = 2f;
        noInteractionTimer = 0f;
    }


    public void SwitchActiveState()
    {
        if (active == ActivationEnum.Off)
        {
            active = ActivationEnum.On;
        }
        else
        {
            active = ActivationEnum.Off;
        }
    }

    public virtual void DestroyTheObject()
    {
        if (!destroy && !inCooldown)
        {
            originalScale = transform.localScale;
            destroy = true;
        }
    }

    public void ResetObject()
    {
        inCooldown = true;
        transform.localScale = originalScale;
    }

    public virtual void Update()
    {
    }

    public virtual void MoveUpAndDown()
    {

    }

    public virtual void MoveSideway()
    {

    }

    public virtual void RotateZ()
    {

    }

    public virtual void RotateY()
    {

    }
}