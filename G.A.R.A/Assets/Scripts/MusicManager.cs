using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] float minRandomStartTime = 1f;
    [SerializeField] float maxRandomStartTime = 10f;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioMixerSnapshot defaultSnapshot;
    [SerializeField] AudioMixerSnapshot softenSnapshot;
    [SerializeField] float transitionTime = 0.5f;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartMusic();
        PauseMusic();
    }

    void Update()
    {

//#if UNITY_EDITOR
//        if (Input.GetKeyDown(KeyCode.Alpha1))
//            TransitionToSoftSnapshot();

//        if (Input.GetKeyDown(KeyCode.Alpha2))
//            ResetSnapshot();
//#endif


    }

    //Play the audioclip of your choice
    public void PlayMusicClip(AudioClip musicClip)
    {
        source.clip = musicClip;
        StopAllCoroutines();
        StartCoroutine(StartMusic());
        source.volume = Mathf.Lerp(0f, 1f, 4f);
    }

    //Smoothly transition to the next snapshot
    public void TransitionToSoftSnapshot()
    {
        softenSnapshot.TransitionTo(transitionTime );
    }

    //Go back to the default snapshot
    public void ResetSnapshot()
    {
        defaultSnapshot.TransitionTo(transitionTime );
    }

    public void PauseMusic()
    {
        StopAllCoroutines();
        source.Pause();
    }

    public void ContinueMusic()
    {
        StartCoroutine(CoolDown());
        source.UnPause();
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(Random.Range(minRandomStartTime, maxRandomStartTime));
        source.Play();
        StartCoroutine(CoolDown());
    }

    //Coroutine that stops the music from starting before its done
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(source.clip.length);
        StartMusic();
    }

}
