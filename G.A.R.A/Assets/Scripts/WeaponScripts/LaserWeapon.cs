
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class LaserWeapon : Weapon
{
    [Header("Visual effects")]
    [SerializeField] private GameObject laserEffect;
    [SerializeField] private GameObject laserHit;
    [Header("Laser properties")]
    [SerializeField] private float maxRange = 100f;
    [SerializeField] private float laserThickness = 0.15f;
    [SerializeField] private float laserDuration = 0.5f;
    [SerializeField] private float inaccuracyFactor;
    private Vector3 shootDirection;



    public override void Shoot()  //Starts visual effects and draw ray to check if colldiding with any valiable target
    {
        base.Shoot();

        RaycastHit hit;
        float x = Random.Range(-inaccuracyFactor, inaccuracyFactor);
        float y = Random.Range(-inaccuracyFactor, inaccuracyFactor);
        shootDirection = firePoint.transform.forward + new Vector3(x,y,0);
        if (Physics.SphereCast(firePoint.position, laserThickness, shootDirection, out hit, maxRange))
        {
            DrawVisuals(hit.point);
            if (hit.transform.CompareTag("Player"))
            {
                //hit.transform.GetComponent<Stats>().TakeDamage(attack);
                Debug.Log("Hit");
            }
        }
        else
        {
            DrawVisuals(Vector3.zero);
        }
    }

    public override void TryShoot()
    {
        if (currentTime <= 0f)
        {
            Shoot();
            anim.SetBool("Fire", true);
            currentTime = timeBetweenAttacks;
        }
    }

    //Draws the laserline
    public override void DrawVisuals(Vector3 target)
    {
        base.DrawVisuals(target);
        laserEffect.GetComponent<LineRenderer>().SetPosition(0, firePoint.position);

        if (target == Vector3.zero)
        {
            laserEffect.GetComponent<LineRenderer>().SetPosition(1, firePoint.position + (shootDirection * maxRange));
        }
        else
        {
            laserEffect.GetComponent<LineRenderer>().SetPosition(1, target);
        }

        StartCoroutine(TurnOffLaserEffect());
    }

    private IEnumerator TurnOffLaserEffect()
    {
        PlayShootSound();
        laserEffect.SetActive(true);
        yield return new WaitForSeconds(laserDuration);
        laserEffect.SetActive(false);
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(firePoint.position, laserThickness);
    }
}
