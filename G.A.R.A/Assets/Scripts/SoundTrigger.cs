using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    //[SerializeField] private AudioClip clipToPlay;


    private Collider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        source.Play();
    }

    void OnTriggerExit(Collider other)
    {
        source.Stop();
    }
    
}
