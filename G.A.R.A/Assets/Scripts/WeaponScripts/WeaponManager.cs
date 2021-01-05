using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject laserWeapon;
    [SerializeField] GameObject explosiveWeapon;
    [SerializeField] GameObject teslaWeapon;

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
        explosiveWeapon.SetActive(false);
        teslaWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }


    void handleInput()   //Check if mouseScroll has changed and thus should change the weapon
    {       
        mouseDelta += (int)Input.mouseScrollDelta.y;

        if (mouseDelta != oldMouseDelta)
        {
           if(mouseDelta > 2)
           {
                mouseDelta = 0;
           }
           else if(mouseDelta < 0)
           {
                mouseDelta = 2;
           }
            Weapons weap = (Weapons)mouseDelta;
            ChangeWeapon(weap);
            oldMouseDelta = mouseDelta;
        }

        if (Input.GetButton("Fire1"))
        {
            currentWeapon.TryShoot();
            
        }

        if(Input.GetButtonDown("Laser"))
        {
            mouseDelta = (int)Weapons.Laser;
            ChangeWeapon(Weapons.Laser);
        }
        else if (Input.GetButtonDown("Explosive"))
        {
            mouseDelta = (int)Weapons.Explosive;
            ChangeWeapon(Weapons.Explosive);
        }
        else if (Input.GetButtonDown("Electric"))
        {
            mouseDelta = (int)Weapons.Taser;
            ChangeWeapon(Weapons.Taser);
        }
    }

    void ChangeWeapon(Weapons weaponToChangeTo)   //Fucntion changing the weapons
    {
        switch (weaponToChangeTo)
        {
            case Weapons.Laser:
                laserWeapon.SetActive(true);
                explosiveWeapon.SetActive(false);
                teslaWeapon.SetActive(false);
                currentWeapon = laserWeapon.GetComponent<Weapon>();
                currentWeapon.PlayStartUpSound();
                break;

            case Weapons.Explosive:
                explosiveWeapon.SetActive(true);
                laserWeapon.SetActive(false);
                teslaWeapon.SetActive(false);
                currentWeapon = explosiveWeapon.GetComponent<Weapon>();
                currentWeapon.PlayStartUpSound();
                break;

            case Weapons.Taser:
                teslaWeapon.SetActive(true);
                explosiveWeapon.SetActive(false);
                laserWeapon.SetActive(false);
                currentWeapon = teslaWeapon.GetComponent<Weapon>();
                currentWeapon.PlayStartUpSound();
                break;
            default:
                //
                break;
        }
    }

    public void ResetAllAmmo()
    {
        laserWeapon.GetComponent<Weapon>().ResetAmmo();
        explosiveWeapon.GetComponent<Weapon>().ResetAmmo();
        teslaWeapon.GetComponent<Weapon>().ResetAmmo();

    }
}
