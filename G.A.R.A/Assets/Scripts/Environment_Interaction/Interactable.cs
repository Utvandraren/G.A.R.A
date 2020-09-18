
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteracted;
    float outlineCountdown = 0;

    public void Interact()
    {
        Debug.Log("Hit!");
        onInteracted?.Invoke();
    }

    public void EnableOutline()
    {
        if(!transform.GetComponent<Outline>().enabled)
        {
            transform.GetComponent<Outline>().enabled = true;
        }
        outlineCountdown = 1f;
    }

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
