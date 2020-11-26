using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleDamageBox : MonoBehaviour
{
    public SciptableAttackObj attack;

    void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent<Stats>(out Stats attackObj))
        {
            if (other.CompareTag("Player"))
                attackObj.TakeContinuousDamage(attack);
        }
    }   
}
