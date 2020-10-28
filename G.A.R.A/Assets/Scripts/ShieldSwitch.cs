using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldSwitch : MonoBehaviour
{
    [SerializeField] private GameObject turnedOnObj;
    [SerializeField] private GameObject turnedOffObj;
    [SerializeField] private GameObject neutralObj;

    bool isActive = false;

    public void Activate()
    {
        Debug.Log(name + " activated");
        isActive = true;
        neutralObj.SetActive(false);
        turnedOnObj.SetActive(false);
        turnedOffObj.SetActive(true);
    }

    public void SwitchOn()
    {
        if (isActive)
        {
            BossManager.Instance.reduceShield();
            turnedOnObj.gameObject.SetActive(true);
            turnedOffObj.gameObject.SetActive(false);
            isActive = false;
        }
    }

    public void ResetSwitch()
    {
        isActive = false;
        neutralObj.SetActive(true);
        turnedOnObj.SetActive(false);
        turnedOffObj.SetActive(false);
    }

}
