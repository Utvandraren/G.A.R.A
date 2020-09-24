using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Door_Interactions : MonoBehaviour
{
    public bool returnToNormalWithSecondInteraction;
    public bool isOpen;

    public float rangeToOpen;

    public void DoorMovesUpwards()
    {
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

    public void DoorMovesSideways()
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
}
