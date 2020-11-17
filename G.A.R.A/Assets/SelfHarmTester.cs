using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHarmTester : MonoBehaviour
{
    PlayerStats playerStats;
    SciptableAttackObj attack;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        attack = new SciptableAttackObj();
        attack.damage = 10;
        attack.element = SciptableAttackObj.WeaponElement.Laser;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            playerStats.TakeDamage(attack);
    }
}
