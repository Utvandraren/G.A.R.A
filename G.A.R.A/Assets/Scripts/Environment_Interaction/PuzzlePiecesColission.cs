using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiecesColission : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("PuzzlePiece"))
        {
            PuzzleManager.Instance.puzzleCompleted = true;
            if(gameObject.TryGetComponent<SwitchMaterial>(out SwitchMaterial sm))
            {
                sm.isBlue = false;
                sm.isGreen = false;
            }
            if (collision.gameObject.TryGetComponent<SwitchMaterial>(out SwitchMaterial smc))
            {
                smc.isBlue = false;
                smc.isGreen = false;
            }
            if(gameObject.TryGetComponent<Interactable>(out Interactable i))
            {
                i.canInteract = false;
                i.showOutline = false;
            }
            if (collision.gameObject.TryGetComponent<Interactable>(out Interactable ic))
            {
                ic.canInteract = false;
                ic.showOutline = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzlePiece"))
        {
            PuzzleManager.Instance.puzzleCompleted = true;
            if (gameObject.TryGetComponent<SwitchMaterial>(out SwitchMaterial sm))
            {
                sm.isBlue = false;
                sm.isGreen = false;
            }
            if (gameObject.TryGetComponent<Interactable>(out Interactable i))
            {
                i.canInteract = false;
                i.showOutline = false;
            }
        }
        if (gameObject.TryGetComponent<MovementInteraction>(out MovementInteraction mi))
        {
            mi.stopCoroutine = true;
        }
    }
}
