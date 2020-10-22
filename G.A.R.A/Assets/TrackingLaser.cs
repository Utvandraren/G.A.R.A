using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingLaser : Weapon
{
    [SerializeField] private float trackingTime = 0f;
    [SerializeField] private float attackDuration = 0f;
    [SerializeField] private GameObject laserBeam;


    private GameObject target;
    private Transform currentTransform;


    // Start is called before the first frame update
    public override void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentDirection = target.transform.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(currentDirection);
        Quaternion neededRotation = Quaternion.LookRotation(currentDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * trackingTime);
    } 

    public override void Shoot()
    {
        base.Shoot();
        StartCoroutine(ActivateLaser());
    }

    public IEnumerator ActivateLaser()
    {
        laserBeam.SetActive(true);
        //PLayVisualEffects
        yield return new WaitForSeconds(attackDuration);
        laserBeam.SetActive(false);

    }
}
