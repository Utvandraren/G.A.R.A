using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleInteraction : MonoBehaviour
{
    public bool returnWithSecondInteract;
    public bool isOpen;

    public float scaleChange;

    private bool firstInteract = true;
    private Vector3 originalScale;

    public void Maximize()
    {
        if (firstInteract)
        {
            firstInteract = false;
            originalScale = transform.localScale;
        }
        if (returnWithSecondInteract)
        {
            if (isOpen)
            {

                transform.localScale = originalScale;
            }
            else
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(scaleChange, scaleChange, scaleChange));
            }

            isOpen = !isOpen;
        }
        else
        {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(scaleChange, scaleChange, scaleChange));
        }

    }
    public void Minimize()
    {
        if (firstInteract)
        {
            firstInteract = false;
            originalScale = transform.localScale;
        }

        if (returnWithSecondInteract)
        {
            if (isOpen)
            {
                transform.localScale = originalScale;
            }
            else
            { 
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1/scaleChange, 1/scaleChange, 1/scaleChange));
            }

            isOpen = !isOpen;
        }
        else
        {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1 / scaleChange, 1 / scaleChange, 1 / scaleChange));
        }

    }
}
