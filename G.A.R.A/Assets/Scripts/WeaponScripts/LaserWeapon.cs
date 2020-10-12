
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class LaserWeapon : Weapon
{
    [SerializeField] private GameObject laserEffect;
    [SerializeField] private float maxRange = 100f;
    [SerializeField] private float laserThickness = 0.15f;


    public override void Shoot()  //Starts visual effects and draw ray to check if colldiding with any valiable target
    {
        base.Shoot();
        GameObject instantLaserEffect;
        instantLaserEffect = Instantiate(laserEffect, firePoint.position, firePoint.rotation,transform);
        Destroy(instantLaserEffect, 0.2f);
        PlayShootSound();

        RaycastHit hit;
        Debug.LogFormat("Shots fired from: " + gameObject.ToString());
        if(Physics.SphereCast(firePoint.position, laserThickness, firePoint.forward, out hit, maxRange))
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


    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(firePoint.position, laserThickness);
    }
}
