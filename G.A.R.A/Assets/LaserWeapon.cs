
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

        //laserEffect.Play();
        //shootSound.Play();

        RaycastHit hit;
        Debug.LogFormat("Shots fired from: " + gameObject.ToString());
        //Debug.DrawRay(firePoint.position, firePoint.forward, Color.red, 1f);  // ta bort denna raden när den inte behövs

        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100f, 0))
        {
            if(hit.transform.CompareTag("enemy"))
            {
                //enemy.takedamage(Damage)    <-- change to the right function when it has been implemented
            }
            else if(hit.transform.CompareTag("obj"))
            {
                //object.interact             <-- change to the right function when it has been implemented
            }
        }
    }

   

    
}
