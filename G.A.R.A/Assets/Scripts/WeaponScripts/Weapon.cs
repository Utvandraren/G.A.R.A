using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public AudioSource shootSound;
    public SciptableAttackObj attack;

    [SerializeField] private float timeBetweenAttacks;
    private float currentTime;

    void Start()
    {
        currentTime = timeBetweenAttacks;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        Mathf.Clamp(currentTime, 0f, timeBetweenAttacks);

    }

    public void TryShoot()  //If cooldown is ready then you can shoot
    {
        currentTime -= Time.deltaTime;
        Mathf.Clamp(currentTime, 0f, timeBetweenAttacks);

        if (currentTime <= 0f)
        {
            Shoot();
            currentTime = timeBetweenAttacks;
        }

    }

    public virtual void Shoot()
    {       
    }

}
