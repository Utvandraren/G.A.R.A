using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserWeapon : Weapon
{
    [SerializeField] private float range;
    [SerializeField] private float jumpRange;
    [SerializeField] private int nmbrJumps;
    [SerializeField] private ParticleSystem electricityEffect;
    [SerializeField] private GameObject electricityHitSparks;


    private LineRenderer line;
    private List<Collider> targetsAlreadyHit;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        targetsAlreadyHit = new List<Collider>();
        line = gameObject.GetComponentInChildren<LineRenderer>();
    }

    public override void Shoot()
    {
        base.Shoot();
        RaycastHit hit;
        Debug.LogFormat("Shots fired from: " + gameObject.ToString());
        electricityEffect.Play();

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            if (hit.transform.TryGetComponent<Interactable>(out Interactable interObj))
            {
                interObj.Interact(attack);
            }
            else if (hit.transform.TryGetComponent<EnemyStats>(out EnemyStats attackObj))
            {
                HandleElectricityArchs(attackObj);
            }
        }
    }

    void HandleElectricityArchs(EnemyStats attackObj) //Handles how the weapon go from enemy to enemy damaging them
    {
        attackObj.TakeDamage(attack);
        targetsAlreadyHit.Add(attackObj.GetComponent<Collider>());

        for (int i = 0; i < nmbrJumps; i++)
        {
            Collider[] colliders = Physics.OverlapSphere(attackObj.transform.position, jumpRange);
            attackObj = GetClosestEnemy(colliders, attackObj.GetComponent<Collider>());

            if (attackObj != null)
            {
                attackObj.TakeDamage(attack);
                targetsAlreadyHit.Add(attackObj.GetComponent<Collider>());
            }
            else
            {
                break;
            }
        }
        //DrawVisualEffects();
        StartCoroutine("VisualEffectCo");
        
    }

    IEnumerator VisualEffectCo()   //Draw the visual effects for the electricity based on the targets that have been hit.    
    {    
        int i = 0;
        List<GameObject> sparksEffectList = new List<GameObject>();

        //Draw line from fireposition to the next target
        foreach (Collider target in targetsAlreadyHit)
        {
            ++line.positionCount;
            line.SetPosition(i, target.transform.position);
            sparksEffectList.Add(Instantiate(electricityHitSparks, target.transform.position, Quaternion.identity));     //Sparkseffekts
            ++i;
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return new WaitForSeconds(1f);
        targetsAlreadyHit.Clear();
        line.positionCount = 0;

        foreach (GameObject effect in sparksEffectList)
        {
            Destroy(effect, 3f);
        }


    }

    //void DrawVisualEffects()  //Draw the visual effects for the electricity based on the targets that have been hit.                        
    //{
    //    //Debug.Log("Number of targets: " + targetsAlreadyHit.Count.ToString());
    //    line.positionCount = targetsAlreadyHit.Count;
    //    int i = 0;
    //    line.SetPosition(i, firePoint.position);

    //    //Draw line from fireposition to the next target
    //    foreach (Collider target in targetsAlreadyHit)
    //    {
    //        line.SetPosition(i, target.transform.position);
    //        ++i;
    //    }
    //}

    bool targetAlreadyHit(Collider collider)
    {
        foreach (Collider hitTarget in targetsAlreadyHit)
        {
            if (hitTarget == collider)
            {
                return true;
            }
        }
        return false;
    }

    EnemyStats GetClosestEnemy(Collider[] colliders, Collider startEnemy)  //Find the enemy closest to the startEnemy 
    {
        Collider closestEnemy = colliders[0];
        for (int i = 0; i < colliders.Length; i++)
        {
            if (Vector3.Distance(startEnemy.transform.position, colliders[i].transform.position) < Vector3.Distance(startEnemy.transform.position, closestEnemy.transform.position) && targetAlreadyHit(colliders[i]) == false /*&& colliders[i] != closestEnemy*/)
            {
                closestEnemy = colliders[i];
            }
        }

        if (closestEnemy.TryGetComponent<EnemyStats>(out EnemyStats attackObj))
        {
            return attackObj;
        }
        else
        {
            Debug.Log("No enemies found");
            return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Gizmos.DrawRay(firePoint.position, firePoint.forward);
        //Gizmos.DrawLine(firePoint.position, new Vector3(firePoint.position.x, firePoint.position.y, range));
    }
}
