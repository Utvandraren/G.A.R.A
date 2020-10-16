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
    [SerializeField] public AudioClip startUpSound;


    private float currentTime;
    private AudioSource weaponAudioSource;
    private System.Random rnd;

    public virtual void Start()
    {
        currentTime = timeBetweenAttacks;
        ammo.ResetValue();
        rnd = new System.Random();
        weaponAudioSource = GetComponent<AudioSource>();
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
        weaponAudioSource.pitch = Random.Range(1f, 1.1f);
        weaponAudioSource.Play();
    }

    void OnEnable()
    {
        weaponAudioSource.PlayOneShot(startUpSound);
    }

}
