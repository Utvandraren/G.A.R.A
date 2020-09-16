using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private GameObject firePoint;

    // Update is called once per frame
    void Update()
    {
        HandleShooterInput();
        UpdateDrawBack();
    }

    void HandleShooterInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public virtual void Shoot()
    {

    }

    public virtual void UpdateDrawBack()
    {

    }
}
