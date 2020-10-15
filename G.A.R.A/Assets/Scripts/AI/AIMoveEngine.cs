using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveEngine : MonoBehaviour
{
    new Rigidbody rigidbody;
    [SerializeField] float speed;
    [SerializeField] float defaultSpeed = 5f;
    [SerializeField] [Range(1, 10)] float degreesPerTick = 2f;
    [SerializeField] [Range(0, 1)] float speedStabilisation = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    internal void Stop()
    {
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, speedStabilisation);
    }

    /// <summary>
    /// Used to turn enemy without movement
    /// </summary>
    /// <param name="target"></param>
    internal void LookTowards(Vector3 target)
    {
        Vector3 relativePos = target - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        rigidbody.MoveRotation(Quaternion.RotateTowards(rigidbody.rotation, toRotation, degreesPerTick));
    }

    internal void DirectionRotateAndMove(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction);
        rigidbody.MoveRotation(Quaternion.RotateTowards(rigidbody.rotation, toRotation, degreesPerTick));
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction * defaultSpeed, speedStabilisation);
    }

    internal void MoveWithoutRotation(Vector3 addedVelocity)
    {
        rigidbody.velocity += addedVelocity;
    }
}
