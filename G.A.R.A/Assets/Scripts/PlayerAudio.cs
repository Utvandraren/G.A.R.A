using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private static AudioSource src;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public static void Play(AudioClip clip, float volume)
    {
        src.PlayOneShot(clip, volume);
    }
}
