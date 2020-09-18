using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public AudioSource shootSound;

    public int damage;
    [SerializeField] private float timeBetweenAttacks;
    
    private float currentTime;

    void Start()
    {
        currentTime = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        HandleShooterInput();      
    }

    void HandleShooterInput()  //If cooldown is ready then you can shoot
    {
        currentTime -= Time.deltaTime;
        Mathf.Clamp(currentTime, 0f, timeBetweenAttacks);

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentTime <= 0f)
            {
                Shoot();
                currentTime = timeBetweenAttacks;
            }
        }
    }

    public virtual void Shoot()
    {
    }

}
