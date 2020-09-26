using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public AudioSource shootSound;
    public SciptableAttackObj attack;

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private SciptableIntObj ammo;

    private float currentTime;

    void Start()
    {
        currentTime = timeBetweenAttacks;
        ammo.ResetValue();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        Mathf.Clamp(currentTime, 0f, timeBetweenAttacks);

    }

    public void TryShoot()  //If cooldown is ready then you can shoot
    {
        //currentTime -= Time.deltaTime;
        //Mathf.Clamp(currentTime, 0f, timeBetweenAttacks);

        if (currentTime <= 0f && AmmoNotEmpty())
        {
            Shoot();
            currentTime = timeBetweenAttacks;
        }

    }

    public virtual void Shoot()
    {       
    }

    bool AmmoNotEmpty()  //Metod checking if ammo is still left
    {
        if(ammo.value > 0)
        {
            return true;
        }
        
        return false;
    }

}
