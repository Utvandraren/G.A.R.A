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
    [SerializeField]float detectionRange = 5f;
    [SerializeField]float maxSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * maxSpeed/2;
        allBoids = FindObjectsOfType<BOID>();
        relevantBoids = new List<BOID>();
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
        acceleration = CalcAcceleration();
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, rigidbody.velocity + acceleration, 0.1f);
        if (rigidbody.velocity.magnitude > maxSpeed)
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity.normalized);
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
                if(Vector3.Dot(transform.forward, normalizedToOther) < 0)
                relevantBoids.Add(other);
        }
    }

    private Vector3 CalcAcceleration()
    {
        Vector3 repulsionVector = Vector3.zero;
        Vector3 allignmentVector = Vector3.zero;
        Vector3 cohesionVector = Vector3.zero;
        foreach (BOID other in relevantBoids)
        {
            repulsionVector += CalcRepulsion(other);
            allignmentVector += CalCAlignment(other);
            cohesionVector += CalcCohesion(other);
        }
        if (relevantBoids.Count > 0)
            return (repulsionVector + allignmentVector + cohesionVector) / relevantBoids.Count;
        else
            return Vector3.zero;
    }


    private Vector3 CalcRepulsion(BOID other)
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
