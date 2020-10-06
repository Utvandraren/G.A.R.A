using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [Tooltip("How long the enemy stays after dying")]
    [SerializeField] private float deathTimer = 0;

    public override void Die()
    {
        base.Die();
        Debug.Log("DEATH TO " + gameObject.ToString());
        Destroy(gameObject, deathTimer);
    }
}
