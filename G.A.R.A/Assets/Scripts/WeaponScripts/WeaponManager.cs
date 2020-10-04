﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject laserWeapon;
    [SerializeField] GameObject explosiveWeapon;
    [SerializeField] GameObject TaserWeapon;

    [SerializeField] private Animator taserAnim;

    Weapon currentWeapon;

    private int mouseDelta;
    private int oldMouseDelta;

    enum Weapons
    {
        Laser = 0,
        Explosive = 1,
        Taser = 2,
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseDelta = 0;
        oldMouseDelta = 0;
        currentWeapon = laserWeapon.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    public void FixedUpdate()
    {
        taserAnim.SetBool("Fire", false);
    }

    void handleInput()   //Check if mouseScroll has changed and thus should change the weapon
    {
        if((int)Input.mouseScrollDelta.y < 0)
        {
            mouseDelta -= (int)Input.mouseScrollDelta.y;
        }
        else
        {
            mouseDelta += (int)Input.mouseScrollDelta.y;
        }
        
        if(mouseDelta != oldMouseDelta)
        {
            mouseDelta = mouseDelta % 3;
            Weapons weap = (Weapons)mouseDelta;
            ChangeWeapon(weap);
            oldMouseDelta = mouseDelta;
        }

        if (Input.GetButton("Fire1"))
        {
            currentWeapon.TryShoot();
            taserAnim.SetBool("Fire", true);
        }
    }

    void ChangeWeapon(Weapons weaponToChangeTo)   //Fucntion changing the weapons
    {
        switch (weaponToChangeTo)
        {
            case Weapons.Laser:
                laserWeapon.SetActive(true);
                explosiveWeapon.SetActive(false);
                TaserWeapon.SetActive(false);
                currentWeapon = laserWeapon.GetComponent<Weapon>();
                break;

            case Weapons.Explosive:
                explosiveWeapon.SetActive(true);
                laserWeapon.SetActive(false);
                TaserWeapon.SetActive(false);
                currentWeapon = explosiveWeapon.GetComponent<Weapon>();
                break;

            case Weapons.Taser:
                TaserWeapon.SetActive(true);
                explosiveWeapon.SetActive(false);
                laserWeapon.SetActive(false);
                currentWeapon = TaserWeapon.GetComponent<Weapon>();
                break;
            default:
                //
                break;
        }
    }
}
