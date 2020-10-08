using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private SciptableIntObj objToModify;
    [SerializeField] private int value = 0;
    [SerializeField] private float deathTimer = 0;
    [SerializeField] private AudioSource pickUpSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerPickupEffects();
        }
    }

    void TriggerPickupEffects()
    {
        objToModify.value += value;
        Destroy(gameObject, deathTimer);
        //pickUpSound.Play();
    }
}
