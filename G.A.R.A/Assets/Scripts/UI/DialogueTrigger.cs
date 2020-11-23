using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// A script that is attached to objects that are going to trigger dialogue. 
/// The dialogue variable contains the text and the text is entered into the inspector
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    private Dialogue dialogue;
    private bool hasBeenInvoked = false;
    private float coolDownTimer = 0;
    private float coolDown = 1f;
    [SerializeField] private TextAsset dialogueFile;
    private bool firstTimeTriggering;
    public string nameOfSpeaker = "";
    [SerializeField] private LogbookCounter lc;

    private DialogueManager dm;
    
    public void Start()
    {
        dialogue = new Dialogue(dialogueFile.text.Split('\n'));
        if(nameOfSpeaker != "")
        {
            dialogue.name = nameOfSpeaker;
        }
        firstTimeTriggering = true;
        hasBeenInvoked = false;
        dm = FindObjectOfType<DialogueManager>();
    }
    /// <summary>
    /// This method is called whenever the player tries to start a dialogue
    /// </summary>
    public void TriggerDialogue()
    {
        if (!hasBeenInvoked)
        {
            if (firstTimeTriggering && gameObject.tag == "Logbook")
            {
                firstTimeTriggering = false;
                GameObject obj = GameObject.Find("LogBookCounter");
                lc.FoundLogbook();
            }
            hasBeenInvoked = true;
            dm.StartDialogue(dialogue);
        }
    }

    public void Update()
    {
        if (!dm.animator.GetBool("IsOpen"))
        {
            coolDownTimer += Time.deltaTime;
            if(coolDownTimer > coolDown)
            {
                hasBeenInvoked = false;
                coolDownTimer = 0;
            }
        }
    }
}
