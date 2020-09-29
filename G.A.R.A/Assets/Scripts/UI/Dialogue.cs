using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //This is a dialogue data-type script, used by DialogueTrigger

    [Tooltip("The name of the NPC or logbook")]
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
