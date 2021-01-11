using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardsTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float trackingSpeed  = 3f;

    //void Start()
    //{
    //    target = GameObject.FindGameObjectWithTag("Player").transform;
    //}

    void LateUpdate()
    {
        handleTargeting();
    }
    //void Update()
    //{
    //    handleTargeting();
    //}

    //Function moving handling how the laser/targetbeam moves around towards the player
    private void handleTargeting()
    {
        //Vector3 currentDirection = target.transform.position - transform.position;
        //Quaternion neededRotation = Quaternion.LookRotation(currentDirection);
        //transform.localRotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * trackingSpeed);

        Vector3 currentDirection = target.transform.position - transform.position;
        Quaternion neededRotation = Quaternion.LookRotation(currentDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * trackingSpeed);
    }
}
