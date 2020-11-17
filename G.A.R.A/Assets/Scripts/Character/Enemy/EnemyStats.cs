using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [Tooltip("How long the enemy stays after dying")]
    [SerializeField] private float deathTimer = .2f;

    [SerializeField] private GameObject deathEffect;

    [SerializeField] private bool isWeakToTesla;
    [SerializeField] private bool isWeakToLaser;
    [SerializeField] private bool isWeakToExplosive;
    [SerializeField] private float weaknessDmgMultiplier;

    public override void TakeDamage(SciptableAttackObj attack)
    {
        switch (attack.element)
        {
            case SciptableAttackObj.WeaponElement.Laser:
                if (isWeakToLaser)
                {
                    health -= (int)(attack.damage * weaknessDmgMultiplier);
                }
                else health -= attack.damage;
                break;
            case SciptableAttackObj.WeaponElement.Explosive:
                if (isWeakToExplosive)
                {
                    health -= (int)(attack.damage * weaknessDmgMultiplier);
                }
                else health -= attack.damage;
                break;
            case SciptableAttackObj.WeaponElement.Electricity:
                if (isWeakToTesla)
                {
                    health -= (int)(attack.damage * weaknessDmgMultiplier);
                }
                else health -= attack.damage;
                break;
            default:
                Debug.Log("Damage type unrecognized by EnemyStats script.");
                health -= attack.damage;
                break;
        }

        Debug.Log(gameObject.ToString() + " is taking damage");
        if (health <= 0f)
        {
            Die();
        }

    }

    public override void Die()
    {
        base.Die();
        GameObject effect = GameObject.Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(effect, 3);
        //Debug.Log("DEATH TO " + gameObject.ToString());
        Destroy(gameObject, deathTimer);
    }
}
