
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class LaserWeapon : Weapon
{
    [SerializeField]private GameObject laserEffect;

    public override void Shoot()  //Starts visual effects and draw ray to check if colldiding with any valiable target
    {
        base.Shoot();
        GameObject instantLaserEffect;
        instantLaserEffect = Instantiate(laserEffect, firePoint.position, firePoint.rotation);
        Destroy(instantLaserEffect, 0.5f);
        //shootSound.Play();

        RaycastHit hit;
        Debug.LogFormat("Shots fired from: " + gameObject.ToString());

        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100f))
        {
            if (hit.transform.TryGetComponent<Interactable>(out Interactable interObj))
            {
                interObj.Interact(attack);
            }

            else if(hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<Stats>().TakeDamage(attack);
            }            
        }
    }
}
