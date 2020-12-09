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
        StartCoroutine(StartMusic());
    }

    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TransitionToSoftSnapshot();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ResetSnapshot();
#endif


    }

    public void PlayMusicClip(AudioClip musicClip)
    {
        source.clip = musicClip;
    }

    public void TransitionToSoftSnapshot()
    {
        softenSnapshot.TransitionTo(transitionTime);
    }

    public void ResetSnapshot()
    {
        defaultSnapshot.TransitionTo(transitionTime);
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

        for (int i = 0; i < 1f / 0.001f; i++)
        {
            yield return new WaitForSeconds(0.1f);
            source.volume += 0.01f;
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(source.clip.length);
        StartMusic();
    }

}
