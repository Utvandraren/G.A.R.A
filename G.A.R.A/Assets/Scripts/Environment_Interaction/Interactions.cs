using UnityEngine;

public class Interactions : MonoBehaviour
{
    public bool returnWithSecondInteract;
    public bool isOpen;

    public float rangeToOpen;

    public float rotationAngle;

    public void MoveUpAndDown()
    {
        
        if (returnWithSecondInteract)
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

    public void RotateAroundZ()
    {
        if (isOpen)
        {
            transform.Rotate(new Vector3(rotationAngle, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(-rotationAngle, 0,0));
        }

        isOpen = !isOpen;
    }
    public void RotateAroundX()
    {
        if (isOpen)
        {
            transform.Rotate(new Vector3(0, rotationAngle, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, -rotationAngle, 0));
        }

        isOpen = !isOpen;
    }
}
