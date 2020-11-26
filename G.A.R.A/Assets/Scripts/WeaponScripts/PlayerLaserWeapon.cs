using System.Collections;
using UnityEngine;

public class PlayerLaserWeapon : Weapon
{
    [Header("Laser effects")]
    [SerializeField] private GameObject laserEffect;
    [SerializeField] private GameObject laserHit;

    [Header("Laser properties")]
    [SerializeField] private float maxRange = 100f;
    [SerializeField] private float laserThickness = 0.15f;
    [SerializeField] private float laserDuration = 0.5f;

    [SerializeField] public Animator anim;

    private Camera camera;
    

    public override void Start()
    {
        base.Start();
        camera = Camera.main;
        //camera = gameObject.GetComponentInChildren<Camera>();
    }

    //Might want to move this to the base class at some point
    private void FixedUpdate()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        anim.SetBool("Fire", false);
    }

    //Draws ray from middle of screen to see if something is hit
    public override void Shoot()  //Starts visual effects and draw ray to check if colldiding with any valiable target
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        base.Shoot();
        anim.CrossFadeInFixedTime("Fire Laser Weapon", 0.01f);
        RaycastHit hit;
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        laserEffect.SetActive(true);
        if (Physics.SphereCast(rayOrigin, laserThickness, camera.transform.forward, out hit, maxRange ))
        {

            DrawVisuals(hit.point);

            if (hit.transform.TryGetComponent<Interactable>(out Interactable interObj) && !hit.transform.CompareTag("Player"))
            {
                interObj.Interact(attack);

            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Stats>().TakeDamage(attack);
            }
        }
        else
        {
            DrawVisuals(Vector3.zero);
        }
    }

    //Draws the laserline
    public override void DrawVisuals(Vector3 target)
    {
        base.DrawVisuals(target);
        
        laserEffect.GetComponent<LineRenderer>().SetPosition(0, firePoint.position);

        if (target == Vector3.zero)
        {
            laserEffect.GetComponent<LineRenderer>().SetPosition(1, camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)) + camera.transform.forward * maxRange);
            
        }
        else
        {
            laserEffect.GetComponent<LineRenderer>().SetPosition(1, target);
            GameObject hitVisualInstance = Instantiate(laserHit, target, Quaternion.identity);
            hitVisualInstance.GetComponent<AudioSource>().Play();
            Destroy(hitVisualInstance, 4f);
        }

        StartCoroutine(TurnOffLaserEffect());
    }

    private IEnumerator TurnOffLaserEffect()
    {
        PlayShootSound();
        //laserEffect.SetActive(true);
        yield return new WaitForSeconds(laserDuration);
        laserEffect.SetActive(false);
    }

    

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(firePoint.position, laserThickness);
    }
}

