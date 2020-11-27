﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Stats : MonoBehaviour
{
    public float health;
    [SerializeField] public int startingHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeDamage(SciptableAttackObj attack)  //Logic handling taking damage
    {
        health -= attack.damage;
        //Debug.Log(gameObject.ToString() + " is taking damage");
        if (health <= 0f)
        {
            Die();
        }
    }

    public virtual void TakeContinuousDamage(SciptableAttackObj attack)
    {
        health -= (attack.damage * Time.deltaTime);
    }

    public virtual void Die()  //virtual function for  whatever happens to the gameobject when it dies
    {
        //DeathsoundEffects or particleeffects?
    }
}
