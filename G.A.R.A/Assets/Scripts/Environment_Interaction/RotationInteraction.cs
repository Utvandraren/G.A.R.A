using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInteraction : MonoBehaviour
{
    public bool returnWithSecondInteract;
    public bool isOpen;

    public float rotationAngle;

    public void RotateZ()
    {
        if (returnWithSecondInteract)
        {
            if (isOpen)
            {
                transform.Rotate(new Vector3(-rotationAngle, 0, 0));
            }
            else
            {
                transform.Rotate(new Vector3(rotationAngle, 0, 0));
            }

            isOpen = !isOpen;
        }
        else
        {
            transform.Rotate(new Vector3(rotationAngle, 0, 0));
        }
        
    }
    public void RotateY()
    {
        if (returnWithSecondInteract)
        {
            if (isOpen)
            {
                transform.Rotate(new Vector3(0, -rotationAngle, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, rotationAngle, 0));
            }

            isOpen = !isOpen;
        }
        else
        {
            transform.Rotate(new Vector3(0, rotationAngle, 0));
        }
        
    }
}
