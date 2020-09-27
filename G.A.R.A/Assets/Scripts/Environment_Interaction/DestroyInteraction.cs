using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles destroy interaction. Either the object is destroyed completely or just set as inactive so we can reset it.
/// </summary>
public class DestroyInteraction : MonoBehaviour
{
    public bool resetable; //If reset should be possible
    public bool isActive;
    private bool destroy; //If object should be destroyed
    private bool firstTime; //If first time entering DestroyMethod.
    private Vector3 originalScale;

    public void Start()
    {
        destroy = false;
        firstTime = true;
    }

    /// <summary>
    /// Invoked when interact() on the interactable object is called. Destroy is changed to true so the update method knows to run
    /// destroy animation.
    /// </summary>
    public void DestroyTheObject()
    {
        if (resetable)
        {
            if (!isActive)
            {
                isActive = !isActive;
                gameObject.SetActive(isActive);
                ResetScale();
            }
            else
            {
                destroy = true;
                isActive = !isActive;
            } 
        }
        else
        {
            destroy = true;
        }
    }

    private void ResetScale()
    {
        transform.localScale = originalScale;
    }

    /// <summary>
    /// Animation which makes the object gradually smaller until it is completely destroyed or set as inactive.
    /// </summary>
    private void DestroyAnimationMinimization()
    {
        if (firstTime)
        {
            originalScale = transform.localScale;
            firstTime = false;
        }
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.97f, 0.97f, 0.97f));

        if (transform.localScale.x / originalScale.x < 0.01 || transform.localScale.y / originalScale.y < 0.01|| 
            transform.localScale.z / originalScale.z < 0.01)
        {
            destroy = false;
            if (resetable)
            {
                gameObject.SetActive(isActive);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Update()
    {
        if (destroy)
        {
            DestroyAnimationMinimization(); //Currently chosen destroy animation
        }
    }
}
