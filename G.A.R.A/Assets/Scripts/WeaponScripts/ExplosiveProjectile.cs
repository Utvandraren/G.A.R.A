using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float explosiveRadius;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private SciptableAttackObj attack;
    [SerializeField] private SciptableAttackObj directHitAttack;
    [SerializeField] private Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        //rigidBody.velocity = transform.forward * speed;
    }



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
            else if (nearbyObj.TryGetComponent<EnemyStats>(out EnemyStats attackObj))
            {
                ////Code here deal damage to enemies caught in the explosion based on the distance to center
                //float distanceToCenter = (int)Vector3.Distance(transform.position, attackObj.transform.position);         
                //distanceToCenter /= explosiveRadius;
                //int oldDamage = attack.damage;
                //float newDamage;
                //newDamage = (float)attack.damage * distanceToCenter;
                //attack.damage = (int)newDamage;
                //Debug.Log("Damage: " + attack.damage.ToString(), attackObj.gameObject);
                //attackObj.TakeDamage(attack);
                //attack.damage = oldDamage;

                attackObj.TakeDamage(attack);
            }
        }
    }

    void OnTriggerEnter(Collider other)  //Damage if possible the obj the projectile collided with and then explode 
    {
        if (other.TryGetComponent<EnemyStats>(out EnemyStats attackObj))
        {
            attackObj.TakeDamage(directHitAttack);
        }

        Explode();
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }

    public void SetDirection(Vector3 direction)
    {

        rigidBody.velocity = direction * speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
