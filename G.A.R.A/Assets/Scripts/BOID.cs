using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOID : MonoBehaviour
{
    Vector3 acceleration;

    new Rigidbody rigidbody;
    BOID[] allBoids;
    List<BOID> relevantBoids;
    [Header("Manuverability")]
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] [Range(0, 1)] float turningRate = 0.1f;
    [SerializeField] [Range(-1, 1)] float detection = -0.25f;

    [Header("Weights")]
    [SerializeField] float seperationWeight = 1f;
    [SerializeField] float allignmentWeight = 1f;
    [SerializeField] float cohesionWeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * maxSpeed / 2;
        allBoids = FindObjectsOfType<BOID>();
        relevantBoids = new List<BOID>();
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
        acceleration = CalcAcceleration();
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, rigidbody.velocity + acceleration, turningRate);
        if (rigidbody.velocity.magnitude > maxSpeed)
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        else if (rigidbody.velocity.magnitude < maxSpeed / 10f)
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed / 10f;
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity.normalized);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void Detect()
    {
        relevantBoids.Clear();
        foreach (BOID other in allBoids)
        {
            if (other == this)
                continue;
            Vector3 toOther = other.transform.position - transform.position;
            Vector3 normalizedToOther = Vector3.Normalize(toOther);
            if (toOther.magnitude < detectionRange)
                if (Vector3.Dot(transform.forward, normalizedToOther) > detection)
                    relevantBoids.Add(other);
        }
    }

    private Vector3 CalcAcceleration()
    {
        Vector3 seperationVector = Vector3.zero;
        Vector3 allignmentVector = Vector3.zero;
        Vector3 cohesionVector = Vector3.zero;
        foreach (BOID other in relevantBoids)
        {
            seperationVector += CalcSeperation(other);
            allignmentVector += CalCAlignment(other);
            cohesionVector += CalcCohesion(other);
        }
        if (relevantBoids.Count > 0)
            return (seperationVector * seperationWeight + allignmentVector * allignmentWeight + cohesionVector * cohesionWeight) / relevantBoids.Count;
        else
            return transform.forward;
    }


    private Vector3 CalcSeperation(BOID other)
    {
        Vector3 awayVector = -(other.transform.position - transform.position);
        awayVector /= (awayVector.sqrMagnitude);
        return awayVector;
    }
    private Vector3 CalCAlignment(BOID other)
    {
        return other.rigidbody.velocity;
    }
    private Vector3 CalcCohesion(BOID other)
    {
        return other.transform.position - transform.position;
    }
}
