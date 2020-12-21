using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosivetrigger : MonoBehaviour
{
    [SerializeField] private float explosiveRadius;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private SciptableAttackObj attack;

    [SerializeField] private GameObject parentGameObj;

    private bool triggerDeatheffect = true;


    void Explode() //The logic handling what happens if a interactable object or enemy is inside the explosion radius when the projectile explode
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        effect.GetComponent<AudioSource>().Play();
        Destroy(effect, 10f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);

        foreach (Collider nearbyObj in colliders)
        {
            if (nearbyObj.TryGetComponent<Interactable>(out Interactable interObj))
            {
                interObj.Interact(attack);
            }
            else if (nearbyObj.TryGetComponent<Stats>(out Stats attackObj))
            {               
                attackObj.TakeDamage(attack);
            }
        }
    }

    void OnTriggerEnter(Collider other)  //Damage if possible the obj the projectile collided with and then explode 
    {
        if (other.CompareTag("Player"))
        {
            triggerDeatheffect = false;
            Explode();
            Destroy(parentGameObj);
            parentGameObj.SetActive(false);

        }

    }

    void OnDestroy()
    {
        if (triggerDeatheffect)
        {
            Explode();
            Destroy(parentGameObj);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
