using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private SciptableIntObj objToModify;
    [SerializeField] private int value = 0;
    [SerializeField] private float deathTimer = 1;
    [SerializeField] private AudioSource pickUpSound;
    [SerializeField] private float pickUpDistance;
    [SerializeField] private float pickUpSpeed;
    [SerializeField] private float lifetime;
    private GameObject player;


    [Header("Child objects")]
    [Tooltip("Add child objects that should be deactivated while the self destructing object plays sounds or finishes animations")]
    [SerializeField] private List<GameObject> childList;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickUpSound = GetComponent<AudioSource>();
        deathTimer = pickUpSound.clip.length;
        Destroy(gameObject, lifetime); //Self-destruct after the lifetime has passed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerPickupEffects();
        }
    }

    private void TriggerPickupEffects()
    {
        pickUpSound.Play();
        
        if (objToModify.value + value >= objToModify.startValue)
        {
            objToModify.value = objToModify.startValue;
        }
        else objToModify.value += value;

        foreach (GameObject child in childList)
        {
            child.SetActive(false);
        }

        Destroy(gameObject, deathTimer);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < pickUpDistance)
        {
            Vector3 direction = player.transform.position - transform.position;
            Vector3.Normalize(direction);
            transform.Translate(direction * pickUpSpeed * Time.deltaTime, Space.World);
        }
    }
}
