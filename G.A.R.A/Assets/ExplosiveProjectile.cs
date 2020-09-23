﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float explosiveRadius;
    [SerializeField] private int damage;
    [SerializeField] private GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void Explode() //The logic handling what happens if a interactable object or enemy is inside the explosion radius when the projectile explode
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 10f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);

        foreach (Collider nearbyObj in colliders)
        {
            if (nearbyObj.gameObject.CompareTag("obj"))   //<-----Problary change this here for a more proper way when whe are sure for how the interactions should work
            {
                nearbyObj.GetComponent<Interactable>().Interact();
            }
            else if (nearbyObj.gameObject.CompareTag("Enemy"))
            {
                EnemyStats enemy = nearbyObj.GetComponent<EnemyStats>();
                enemy.TakeDamage(damage);
            }          
        }
    }
     
    void OnTriggerEnter(Collider other) 
    {
        Explode();
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
