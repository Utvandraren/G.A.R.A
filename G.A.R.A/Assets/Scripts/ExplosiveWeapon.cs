using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWeapon : Weapon
{
    [SerializeField] GameObject projectile;

    public override void Shoot()
    {
        base.Shoot();
        Instantiate(projectile, firePoint.position, transform.rotation);
    }

   
}
