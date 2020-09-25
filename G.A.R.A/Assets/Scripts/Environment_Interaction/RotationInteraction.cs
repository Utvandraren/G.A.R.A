using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInteraction : MonoBehaviour
{
    public bool returnWithSecondInteract;
    public bool isFullyRotated;

    public float rotationAngle;

    /// <summary>
    /// Rotates around Z when invoked. Either returns to normal with second invoke or continues rotating.
    /// </summary>
    public void RotateZ()
    {
        if (returnWithSecondInteract)
        {
            if (isFullyRotated)
            {
                transform.Rotate(new Vector3(-rotationAngle, 0, 0));
            }
            else
            {
                transform.Rotate(new Vector3(rotationAngle, 0, 0));
            }

            isFullyRotated = !isFullyRotated;
        }
        else
        {
            transform.Rotate(new Vector3(rotationAngle, 0, 0));
        }
        
    }

    /// <summary>
    /// Rotates around Y when invoked. Either returns to normal with second invoke or continues rotating.
    /// </summary>
    public void RotateY()
    {
        if (returnWithSecondInteract)
        {
            if (isFullyRotated)
            {
                transform.Rotate(new Vector3(0, -rotationAngle, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, rotationAngle, 0));
            }

            isFullyRotated = !isFullyRotated;
        }
        else
        {
            transform.Rotate(new Vector3(0, rotationAngle, 0));
        }
        
    }
}
