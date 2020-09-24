using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Stats : MonoBehaviour
{
    private int Health;
    [SerializeField] private int StartingHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Health = StartingHealth;
    }

    public virtual void TakeDamage(SciptableAttackObj attack)  //Logic handling taking damage
    {
        Health -= attack.damage;
        Debug.Log(gameObject.ToString() + " is taking damage");
        if (Health <= 0f)
        {
            Die();
        }
    }

    public virtual void Die()  //virtual function for  whatever happens to the gameobject when it dies
    {
        //DeathsoundEffects or particleeffects?
    }
}
