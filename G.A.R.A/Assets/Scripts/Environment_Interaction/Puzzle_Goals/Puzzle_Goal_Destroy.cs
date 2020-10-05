using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Goal_Destroy : MonoBehaviour
{
    public Material completedMaterial;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(PuzzleManager.Instance.puzzleCompleted == true)
        {
            PuzzleManager.Instance.puzzleCompleted = false;
            if(gameObject.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.canInteract = true;
                interactable.showOutline = true;
                gameObject.GetComponent<MeshRenderer>().material = completedMaterial;
                //interactable.showOutline = true;
            }
            if(gameObject.TryGetComponent<DestroyInteraction>(out DestroyInteraction di))
            {
                di.destroyable = true;
            }
        }
    }
}
