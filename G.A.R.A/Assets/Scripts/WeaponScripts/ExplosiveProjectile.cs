using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float explosiveRadius;
    [SerializeField] private int damage;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private SciptableAttackObj attack;

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
            if(nearbyObj.TryGetComponent<Interactable>(out Interactable interObj))
            {
                interObj.Interact(attack);
            }
            else if (nearbyObj.TryGetComponent<EnemyStats>(out EnemyStats attackObj))
            {
                attackObj.TakeDamage(attack);
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
