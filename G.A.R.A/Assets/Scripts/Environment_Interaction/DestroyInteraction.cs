using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Handles destroy interaction. Either the object is destroyed completely or just set as inactive so we can reset it.
/// </summary>
public class DestroyInteraction : MonoBehaviour
{
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

    /// <summary>
    /// Invoked when interact() on the interactable object is called. Destroy is changed to true so the update method knows to run
    /// destroy animation.
    /// </summary>
    public void DestroyTheObject()
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

    /// <summary>
    /// Animation which makes the object gradually smaller until it is completely destroyed or set as inactive.
    /// </summary>
    private void DestroyAnimationMinimization()
    {
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.97f, 0.97f, 0.97f));

        if (transform.localScale.x / originalScale.x < 0.05 || transform.localScale.y / originalScale.y < 0.05 ||
            transform.localScale.z / originalScale.z < 0.05)
        {
            destroy = false;
            PuzzleManager.Instance.AddDeletedObjectToQueue(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (inCooldown)
        {
            noInteractionTimer += Time.deltaTime;
            if (noInteractionTimer >= noInteractionCooldown)
            {
                inCooldown = false;
                noInteractionTimer = 0;
            }
        }

        if (destroy)
        {
            DestroyAnimationMinimization(); //Currently chosen destroy animation
        }
    }
}
