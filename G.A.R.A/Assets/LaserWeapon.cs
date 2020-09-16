using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class LaserWeapon : Weapon
{
    public override void Shoot()
    {
        base.Shoot();
        // Draw line
        //if line hit check tag of hit object
        //If interactable object  -> call interactfunction inside it?
    }

    public override void UpdateDrawBack()
    {
        base.UpdateDrawBack();
        Debug.DrawLine(transform.position, new Vector3(transform.position.x * 100f, 0, 0),Color.red);  // ta bort denna raden när spelet den inte behövs
    }

    
}
