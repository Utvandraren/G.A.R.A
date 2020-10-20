using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handles all dialogue. 
/// The animation of the dialogue box, the text and keeps tabs on when the dialogue has run out
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Animator animator;
    private PlayerInteractController interact;

    private Queue<string> sentences;

    // Start is called before the first frame update
    private void Start()
    {
        sentences = new Queue<string>();
        interact = FindObjectOfType<PlayerInteractController>();
    }

    /// <summary>
    /// Checks if the player is trying to advance currently running dialogue 
    /// or if they go to far from the dialogue source it ends the dialogue
    /// </summary>
    private void Update()
    {
        if (animator.GetBool("IsOpen"))
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                DisplayNextSentence();
            }
        }

        //if(!interact.canInteract)
        //{
        //    EndDialogue();
        //}
    }

    /// <summary>
    /// This is called by the DialogueTrigger script when the player starts a dialogue
    /// </summary>
    /// <param name="dialogue">Dialogue variable from the object that contains the dialogue text</param>
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// This method advances the dialogue/log entry to display the next piece of text
    /// </summary>
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //StopCoroutine(TypeSentence(sentence));
        //StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Animates the text one letter at a time
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    private IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; //Might change this later to waitforseconds
        }
    }

    /// <summary>
    /// This method makes the dialogue box close.
    /// </summary>
    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
