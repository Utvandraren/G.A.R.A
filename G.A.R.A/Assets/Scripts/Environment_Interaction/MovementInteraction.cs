using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInteraction : MonoBehaviour
{
    public bool reverseWithSecondInteract;
    public bool isFullyMoved;

    public float rangeToOpen;

    /// <summary>
    /// Moves the object along y-axis when invoked. Moves object back with second invoke or continues moving in same direction.
    /// </summary>
    public void MoveUpAndDown()
    {
        if (reverseWithSecondInteract)
        {
            //If second interact with object should reverse direction
            if (isFullyMoved)
            {
                transform.position = transform.position - new Vector3(0, rangeToOpen, 0);

            }
            else
            {
                transform.position = transform.position + new Vector3(0, rangeToOpen, 0);
            }

            isFullyMoved = !isFullyMoved;
        }
        else
        {
            //If second interact with object is same direction
            transform.position = transform.position + new Vector3(0, rangeToOpen, 0);
        }

    }

    /// <summary>
    /// Moves the object along x-axis when invoked. Moves object back with second invoke or continues moving in same direction.
    /// </summary>
    public void MoveSideway()
    {
        if (reverseWithSecondInteract)
        {
            if (isFullyMoved)
            {
                transform.position = transform.position - new Vector3(rangeToOpen, 0, 0);

            }
            else
            {
                transform.position = transform.position + new Vector3(rangeToOpen, 0, 0);
            }

            isFullyMoved = !isFullyMoved;
        }
        else
        {
            transform.position = transform.position + new Vector3(rangeToOpen, 0, 0);
        }
        
    }
}
