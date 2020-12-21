using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActivationHandleSound : MonoBehaviour
{
    [SerializeField]
    AudioClip clipToPlay;

    [SerializeField]
    Behavior behaviorToPlay;

    [SerializeField]
    WhenToPlay whenToPlay;


    enum WhenToPlay
    {
        Onstart,
        OnEnable
    }

    enum Behavior
    {
        Play,
        Pause,
        ContinuePlaying
    }

    void Start()
    {
        switch (whenToPlay)
        {
            case WhenToPlay.Onstart:
                PlayBehavior();
                break;       
            default:
                break;
        }
    }

    void OnEnable()
    {
        switch (whenToPlay)
        {
            case WhenToPlay.OnEnable:
                PlayBehavior();
                break;
            default:
                break;
        }
    }

    void PlayBehavior()
    {
        switch (behaviorToPlay)
        {
            case Behavior.Play:
                MusicManager.Instance.PlayMusicClip(clipToPlay);
                break;

            case Behavior.Pause:
                MusicManager.Instance.PauseMusic();
                break;

            case Behavior.ContinuePlaying:
                MusicManager.Instance.ContinueMusic();
                break;

            default:
                break;
        }
    }
}

