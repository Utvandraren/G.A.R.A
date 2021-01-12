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
    public Animator animator;
    private PlayerInteractController interactCtrl;

    private Queue<string> sentences;
    private Queue<AudioClip> clips;
    private AudioSource audio;
    private bool useAudio;

    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        sentences = new Queue<string>();
        clips = new Queue<AudioClip>();
        interactCtrl = FindObjectOfType<PlayerInteractController>();
    }

    /// <summary>
    /// Checks if the player is trying to advance currently running dialogue 
    /// or if they go to far from the dialogue source it ends the dialogue
    /// </summary>
    private void Update()
    {
        if (animator.GetBool("IsOpen"))
        {
            if(Input.GetButtonDown("Use"))
            {
                DisplayNextSentence();
            }
        }

        try
        {
            if (!interactCtrl.canInteract && animator.GetBool("IsOpen"))
            {
                EndDialogue();
            }

        }
        catch (System.Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        
    }

    /// <summary>
    /// This is called by the DialogueTrigger script when the player starts a dialogue
    /// </summary>
    /// <param name="dialogue">Dialogue variable from the object that contains the dialogue text</param>
    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        sentences.Clear();
        clips.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if(dialogue.clips != null)
        {
            useAudio = true;
            foreach (AudioClip clip in dialogue.clips)
            {
                clips.Enqueue(clip);
            }
        }
        else
        {
            useAudio = false;
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
        audio.Stop();
        if(useAudio)
        {
            AudioClip clip = clips.Dequeue();
            audio.PlayOneShot(clip);
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
    /// This method makes the dialogue box close. Resumes the game and stops any sound that is playing.
    /// </summary>
    private void EndDialogue()
    {
        audio.Stop();
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        animator.SetBool("IsOpen", false);
    }
}
