
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class LaserWeapon : Weapon
{
    [SerializeField]private ParticleSystem laserEffect;

    public override void Shoot()  //Starts visual effects and draw ray to check if colldiding with any valiable target
    {
        base.Shoot();

        laserEffect.Play();
        //shootSound.Play();

        RaycastHit hit;
        Debug.LogFormat("Shots fired from: " + gameObject.ToString());

        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100f))
        {
            if(hit.transform.CompareTag("enemy"))
            {
                //enemy.takedamage(Damage)    <-- change to the right function when it has been implemented
            }
            else if(hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<GunInteractTest>().Interact();
                //object.interact             <-- change to the right function when it has been implemented
            }
        }
    }

   

    
}
