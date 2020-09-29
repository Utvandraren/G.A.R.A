using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that is attached to objects that are going to trigger dialogue. 
/// The dialogue variable contains the text and the text is entered into the inspector
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    /// <summary>
    /// This method is called whenever the player tries to start a dialogue
    /// </summary>
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
