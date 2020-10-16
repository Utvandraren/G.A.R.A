using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleDamageBox : MonoBehaviour
{
    public SciptableAttackObj attack;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Stats>(out Stats attackObj))
        {
            attackObj.TakeDamage(attack);
        }
    }   
}
