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
    
    public void Start()
    {
        dialogue = new Dialogue(dialogueFile.text.Split('\n'));
        firstTimeTriggering = true;
    }
    /// <summary>
    /// This method is called whenever the player tries to start a dialogue
    /// </summary>
    public void TriggerDialogue()
    {
        if (!hasBeenInvoked)
        {
            if (firstTimeTriggering)
            {
                firstTimeTriggering = false;
                GameObject obj = GameObject.Find("LogBookCounter");
                if(obj.TryGetComponent<LogbookCounter>(out LogbookCounter lc))
                {
                    lc.FoundLogbook();
                }
            }
            hasBeenInvoked = true;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    public void Update()
    {
        if (hasBeenInvoked)
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
