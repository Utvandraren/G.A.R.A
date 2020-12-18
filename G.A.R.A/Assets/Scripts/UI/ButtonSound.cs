using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is just used together with Unity animations for buttons
/// </summary>
public class ButtonSound : MonoBehaviour
{
    private AudioSource audio;

    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void PlayHoverSound()
    {
        audio.PlayOneShot(hoverSound);
    }

    private void PlayClickSound()
    {
        audio.PlayOneShot(clickSound);
    }
}
