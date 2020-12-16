using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActivationHandleSound : MonoBehaviour
{
    [SerializeField]
    AudioClip clipToPlay;

    [SerializeField]
    Behavior behaviorToPlay;

    enum Behavior
    {
        play,
        pause,
        continuePlaying
    }

    void OnEnable()
    {
        switch (behaviorToPlay)
        {
            case Behavior.play:
                MusicManager.Instance.PlayMusicClip(clipToPlay);
                break;

            case Behavior.pause:
                MusicManager.Instance.PauseMusic();
                break;

            case Behavior.continuePlaying:
                MusicManager.Instance.ContinueMusic();
                break;
        }
    }
}

