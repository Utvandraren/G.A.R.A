using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject laserWeapon;
    [SerializeField] GameObject explosiveWeapon;
    [SerializeField] GameObject TaserWeapon;

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

    void handleInput()   //Check if mouseScroll has changed and thus should change the weapon
    {
        mouseDelta += (int)Input.mouseScrollDelta.y;
        mouseDelta = (int)Mathf.Clamp(mouseDelta, 0f, 2f);
        Weapons weap = (Weapons)mouseDelta;

        if(mouseDelta != oldMouseDelta)
        {
            ChangeWeapon(weap);
            oldMouseDelta = mouseDelta;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            currentWeapon.HandleShooterInput();
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
