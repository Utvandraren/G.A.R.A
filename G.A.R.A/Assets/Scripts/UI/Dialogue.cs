using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //This is a dialogue data-type script, used by DialogueTrigger

    [Tooltip("The name of the NPC or logbook")]
    public string name;

    public string[] sentences;

    public AudioClip[] clips { get; private set; }

    public Dialogue(string[] textFileSentences)
    {
        sentences = new string[textFileSentences.Length];
        sentences = textFileSentences;
    }

    public Dialogue(string[] textFileSentences, AudioClip[] clips)
    {
        sentences = new string[textFileSentences.Length];
        sentences = textFileSentences;
        this.clips = clips;
    }

    public void FillSentences(string[] textFileSentences)
    {
        sentences = new string[textFileSentences.Length];
        sentences = textFileSentences;
    }
}
