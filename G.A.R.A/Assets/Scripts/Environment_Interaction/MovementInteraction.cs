using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInteraction : MonoBehaviour
{
    public bool reverseWithSecondInteract;
    public bool isOpen;

    public float rangeToOpen;

    public void MoveUpAndDown()
    {
        if (reverseWithSecondInteract)
        {
            //If second interact with object should reverse direction
            if (isOpen)
            {
                transform.position = transform.position - new Vector3(0, rangeToOpen, 0);

            }
            else
            {
                transform.position = transform.position + new Vector3(0, rangeToOpen, 0);
            }

            isOpen = !isOpen;
        }
        else
        {
            //If second interact with object is same direction
            transform.position = transform.position + new Vector3(0, rangeToOpen, 0);
        }

    }

    public void MoveSideway()
    {
        if (reverseWithSecondInteract)
        {
            if (isOpen)
            {
                transform.position = transform.position - new Vector3(rangeToOpen, 0, 0);

            }
            else
            {
                transform.position = transform.position + new Vector3(rangeToOpen, 0, 0);
            }

            isOpen = !isOpen;
        }
        else
        {
            transform.position = transform.position + new Vector3(rangeToOpen, 0, 0);
        }
        
    }
}
