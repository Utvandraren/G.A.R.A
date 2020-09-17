using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject laserWeapon;
    [SerializeField] GameObject explosiveWeapon;
    [SerializeField] GameObject TaserWeapon;



    Weapons currentWeapon;
    int mouseDelta;
    int oldMouseDelta;

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
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    void handleInput()
    {
        mouseDelta += (int)Input.mouseScrollDelta.y;
        mouseDelta = (int)Mathf.Clamp(mouseDelta, 0f, 2f);
        Weapons weap = (Weapons)mouseDelta;

        if(mouseDelta != oldMouseDelta)
        {
            ChangeWeapon(weap);
            oldMouseDelta = mouseDelta;
        }
    }

    void ChangeWeapon(Weapons weaponToChange)
    {
        switch (weaponToChange)
        {
            case Weapons.Laser:
                laserWeapon.SetActive(true);
                explosiveWeapon.SetActive(false);
                TaserWeapon.SetActive(false);
                break;

            case Weapons.Explosive:
                explosiveWeapon.SetActive(true);
                laserWeapon.SetActive(false);
                TaserWeapon.SetActive(false);
                break;

            case Weapons.Taser:
                TaserWeapon.SetActive(true);
                explosiveWeapon.SetActive(false);
                laserWeapon.SetActive(false);
                break;
            default:
                //
                break;
        }
    }
}
