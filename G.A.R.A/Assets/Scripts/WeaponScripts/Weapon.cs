using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public SciptableAttackObj attack;

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private SciptableIntObj ammo;
    [SerializeField] public Animator anim;

    private float currentTime;
    private AudioSource shootSound;
    private System.Random rnd;

    public virtual void Start()
    {
        currentTime = timeBetweenAttacks;
        ammo.ResetValue();
        rnd = new System.Random();
        shootSound = GetComponent<AudioSource>();
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
            anim.SetBool("Fire", true);
            currentTime = timeBetweenAttacks;
            ammo.value--;
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
        else
        {
            Debug.Log("Ammo Empty " + ammo.value.ToString() + " " + ammo.name);
            return false;
        }
        
    }

    public virtual void DrawVisuals(Vector3 target)
    {

    }

    public void PlayShootSound()
    {
        shootSound.pitch = Random.Range(1f, 1.1f);
        shootSound.Play();
    }

}
