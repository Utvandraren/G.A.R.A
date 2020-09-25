
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A script attached to objects which are interactable. Creates an UnityEvent that is invoked when the object is interacted with.
/// The methods that are Invoked are selected in the Unity editor. 
/// </summary>
public class Interactable : MonoBehaviour
{
    public UnityEvent laserInteraction;
    public UnityEvent explosiveInteraction;
    public UnityEvent electricInteraction;
    float outlineCountdown = 0;

    /// <summary>
    /// Called when the object is interacted with. Invokes the methods selected in the Unity editor.
    /// </summary>
    public void Interact(SciptableAttackObj attack)
    {
        switch (attack.element)
        {
            case SciptableAttackObj.WeaponElement.Laser:
                laserInteraction.Invoke();
                break;
            case SciptableAttackObj.WeaponElement.Explosive:
                explosiveInteraction.Invoke();
                break;
            case SciptableAttackObj.WeaponElement.Electricity:
                electricInteraction.Invoke();
                break;
        }
    }

    /// <summary>
    /// Enables the outline on the object.
    /// </summary>
    public void EnableOutline()
    {
        if(!transform.GetComponent<Outline>().enabled)
        {
            transform.GetComponent<Outline>().enabled = true;
        }
        outlineCountdown = 1f;
    }

    /// <summary>
    /// Disables the outline after one second. The countdown starts when the object is not focused anymore.
    /// </summary>
    public void Update()
    {
        if(outlineCountdown <= 0)
        {
            if (transform.GetComponent<Outline>().enabled)
            {
                transform.GetComponent<Outline>().enabled = false;
            }
        }
        else
        {
            outlineCountdown -= Time.deltaTime;
        }
    }
}
