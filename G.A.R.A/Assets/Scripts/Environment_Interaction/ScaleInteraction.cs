using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scales the obejct when invoked. Either maximizes or minimizes.
/// </summary>
public class ScaleInteraction : MonoBehaviour
{
    public bool returnWithSecondInteract; //If the object should return to original scale with seond interaction.
    public bool isFullyScaled;

    public float scaleChange; //How much the scale should change.

    private bool firstInteract = true;
    private Vector3 originalScale;

    /// <summary>
    /// Scales up the object when invoked. On second invoke either returns to normal or continues to scale
    /// </summary>
    public void Maximize()
    {
        if (firstInteract)
        {
            firstInteract = false;
            originalScale = transform.localScale;
        }
        if (returnWithSecondInteract)
        {
            if (isFullyScaled)
            {

                transform.localScale = originalScale;
            }
            else
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(scaleChange, scaleChange, scaleChange));
            }

            isFullyScaled = !isFullyScaled;
        }
        else
        {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(scaleChange, scaleChange, scaleChange));
        }

    }

    /// <summary>
    /// Scales down the object when invoked. On second invoke either returns to normal or continues to scale
    /// </summary>
    public void Minimize()
    {
        if (firstInteract)
        {
            firstInteract = false;
            originalScale = transform.localScale;
        }

        if (returnWithSecondInteract)
        {
            if (isFullyScaled)
            {
                transform.localScale = originalScale;
            }
            else
            { 
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1/scaleChange, 1/scaleChange, 1/scaleChange));
            }

            isFullyScaled = !isFullyScaled;
        }
        else
        {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1 / scaleChange, 1 / scaleChange, 1 / scaleChange));
        }

    }
}
