
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    public Transform camera;
    public float maxInteractDistance;
    public float maxOutlineDistance;

    private void Update()
    {
        RaycastForOutline();
        RaycastForInteract();
    }

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
