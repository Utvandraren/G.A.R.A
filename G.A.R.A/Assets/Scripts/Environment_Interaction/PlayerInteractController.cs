
using UnityEngine;
/// <summary>
/// A class that handles interaction between the player and interactable objects.
/// </summary>
public class PlayerInteractController : MonoBehaviour
{
    public Transform camera; //The first person camera
    public float maxInteractDistance;
    public float maxOutlineDistance;

    private void Update()
    {
        RaycastForOutline();
        RaycastForInteract();
    }
    /// <summary>
    /// Raycast when interact key is pressed. Checks if the object hit is interactable and if the distance to the object is
    /// closer than maxinteractdistance. Calls the function interact on the interactable object.
    /// </summary>
    private void RaycastForInteract()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit raycastHitInteract;
            if (Physics.Raycast(camera.position, camera.forward, out raycastHitInteract))
            {
                var interactable = raycastHitInteract.transform.GetComponent<Interactable>();

                if (interactable != null)
                {
                    float distance = Vector3.Distance(camera.position, interactable.transform.position);
                    if (distance <= maxInteractDistance)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Raycast to see if interactable object is hit. If it is hit and the object is closer than max distance the outline is enabled.
    /// 
    /// </summary>
    private void RaycastForOutline()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(camera.position, camera.forward, out raycastHit))
        {
            var interactable = raycastHit.transform.GetComponent<Interactable>();

            if (interactable != null)
            {
                float distance = Vector3.Distance(camera.position, interactable.transform.position);
                if (distance <= maxOutlineDistance)
                {
                    interactable.EnableOutline();
                }
            }
        }
    }
}
