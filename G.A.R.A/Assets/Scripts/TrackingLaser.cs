using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingLaser : Weapon
{
    [SerializeField] private float trackingSpeed = 0f; //Original 2
    [SerializeField] private float chargeUpTime = 0f; //Original 2
    [SerializeField] private float attackDuration = 0f;
    [SerializeField] private float laserBeamAttackRadius = 1;
    [SerializeField] private GameObject laserBeam;
    [SerializeField] private GameObject aimingBeam;
    [SerializeField] private ParticleSystem chargeLaserParticlesEffect;



    private GameObject target;
    private BossMovement bossMov;
    private bool currentlyShooting;

    // Start is called before the first frame update
    public override void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        currentlyShooting = false;
        base.Start();
        bossMov = GetComponentInParent<BossMovement>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        handleTargeting();
        //TryShoot();
    }

    public override void TryShoot()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, laserBeamAttackRadius, transform.forward, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                base.TryShoot();
    }
            else
            {
                //Debug.Log(hit.transform.name);
            }
        }
    }

    public override void Shoot()
    {
        
        StartCoroutine(ChargeLaser());
    }

    //Function moving handling how the laser/targetbeam moves around towards the player
    private void handleTargeting()
    {
        if (currentlyShooting)   //Stop the targeting from moving while moving
        {
            return;
        }
        Vector3 currentDirection = target.transform.position - transform.position;
        //transform.forward = Vector3.Lerp(target.transform.position, transform.position, 1);
        Quaternion neededRotation = Quaternion.LookRotation(currentDirection);
        //transform.LookAt(target.transform);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * trackingSpeed);
        //transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * trackingSpeed);
    }
    
    //Plays the chargeup effects warning the player an attack is imminent before the laser is activated
    private IEnumerator ChargeLaser()
    {
        PlayStartUpSound();
        chargeLaserParticlesEffect.Play();
        currentlyShooting = true;
        bossMov.enabled = false;        
        yield return new WaitForSeconds(chargeUpTime);
        StartCoroutine(ActivateLaser());
    }

    //Activates the laser that damage the player
    private IEnumerator ActivateLaser()
    {       
        laserBeam.SetActive(true);
        aimingBeam.SetActive(false);
        PlayShootSound();
        yield return new WaitForSeconds(attackDuration);
        StopShootSound();
        currentlyShooting = false;
        bossMov.enabled = true;
        laserBeam.SetActive(false);
        aimingBeam.SetActive(true);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
