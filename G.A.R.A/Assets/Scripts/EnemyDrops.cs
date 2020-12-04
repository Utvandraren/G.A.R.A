using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public GameObject laserAmmoPickup;
    public GameObject teslaAmmoPickup;
    public GameObject explosiveAmmoPickup;

    private List<GameObject> ammoList;

    private GameObject currentAmmoToDrop;

    public bool dropAllowed;

    private void Start()
    {
        ammoList = new List<GameObject>();
        ammoList.Add(laserAmmoPickup);
        ammoList.Add(teslaAmmoPickup);
        ammoList.Add(explosiveAmmoPickup);
    }

    public void SetLaserDrop()
    {
        currentAmmoToDrop = laserAmmoPickup;
    }
    public void SetTeslaDrop()
    {
        currentAmmoToDrop = teslaAmmoPickup;
    }
    public void SetExplosiveDrop()
    {
        currentAmmoToDrop = explosiveAmmoPickup;
    }


    public void DropAmmo(Vector3 spawnPos)
    {
        //For now, if or when AI director decides ammo spawn
        if(currentAmmoToDrop == null)
        {
            currentAmmoToDrop = ammoList[Random.Range(0, ammoList.Count)].gameObject;
        }

        if(dropAllowed)
            Instantiate(currentAmmoToDrop, spawnPos, Quaternion.identity);
    }
}
