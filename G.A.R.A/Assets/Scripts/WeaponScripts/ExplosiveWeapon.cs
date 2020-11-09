using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWeapon : Weapon
{
    [SerializeField] private GameObject projectile;

    private Camera camera;

    public override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    //Might want to move this to the base class at some point
    private void FixedUpdate()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    //Sends ray to get what direction projectile needs to shoot into the middle of the crosshair
    public override void Shoot()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        base.Shoot();
        PlayShootSound();

        GameObject projectileInstance = Instantiate(projectile, firePoint.position, transform.rotation);
        RaycastHit hit;
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.SphereCast(rayOrigin, 0.15f, camera.transform.forward, out hit, Mathf.Infinity))
        {
            DrawVisuals(hit.point);
            Vector3 direction = hit.point - rayOrigin;
            direction = direction.normalized;
            projectileInstance.GetComponent<ExplosiveProjectile>().SetDirection(direction);
        }
        else
        {
            Vector3 direction = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)) + camera.transform.forward * 2000f;
            direction = direction.normalized;
            projectileInstance.GetComponent<ExplosiveProjectile>().SetDirection(direction);
        }
    }

    public override void DrawVisuals(Vector3 target)
    {
        base.DrawVisuals(target);
    }


}
