using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardsTarget : MonoBehaviour
{
    Transform target;

    [SerializeField]
    float trackingSpeed  = 3f;

    void Start()
    {
        target = FindObjectOfType<PlayerStats>().transform;
    }

    //Function moving handling how the laser/targetbeam moves around towards the player
    private void handleTargeting()
    {
        Vector3 currentDirection = target.transform.position - transform.position;
        Quaternion neededRotation = Quaternion.LookRotation(currentDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * trackingSpeed);
    }
}
