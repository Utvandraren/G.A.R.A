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
    float detectionRangeSqrd;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] [Range(0, 1)] float turningRate = 0.1f;
    [SerializeField] [Range(-1, 1)] float detection = -0.25f;

    [Header("Weights")]
    [SerializeField] float seperationWeight = 1f;
    [SerializeField] float allignmentWeight = 1f;
    [SerializeField] float cohesionWeight = 1f;
    [SerializeField] float avoidWeight = 10f;
    private float maxSteerForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * maxSpeed / 2;
        allBoids = FindObjectsOfType<BOID>();
        relevantBoids = new List<BOID>();
        detectionRangeSqrd = Mathf.Pow(detectionRange, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
        acceleration = CalcAcceleration();
        acceleration += AvoidObstacle() * avoidWeight;
        Debug.Log("pre: " + acceleration.magnitude);
        acceleration = Vector3.ClampMagnitude(acceleration, maxSteerForce);
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, rigidbody.velocity + acceleration, turningRate);
        if (rigidbody.velocity.sqrMagnitude > Mathf.Pow(maxSpeed, 2))
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        else if (rigidbody.velocity.sqrMagnitude < Mathf.Pow(maxSpeed / 10f, 2))
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed / 10f;
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
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
            if (toOther.sqrMagnitude < detectionRangeSqrd)
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
        Vector3 tempAcceleration;
        if (relevantBoids.Count > 0)
            tempAcceleration = (seperationVector * seperationWeight +
                                allignmentVector * allignmentWeight +
                                cohesionVector * cohesionWeight) /
                                relevantBoids.Count;
        else
            tempAcceleration = transform.forward;
        return tempAcceleration;
    }

    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * maxSpeed - rigidbody.velocity;
        return Vector3.ClampMagnitude(v, maxSteerForce);
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

    private Vector3 AvoidObstacle()
    {
        RaycastHit hit;
        if (!Physics.SphereCast(transform.position, 0.05f, transform.forward, out hit, detectionRange * 2, LayerMask.GetMask("Wall")))
            return Vector3.zero;
        Vector3[] obstAvoidRayDirs = BoidManager.CollisionRayDirections;
        float shortestDistToObst = float.MaxValue;
        for (int i = 0; i < obstAvoidRayDirs.Length; i++)
        {
            RaycastHit searchHit;
            Vector3 dir = obstAvoidRayDirs[i];
            Ray ray = new Ray(transform.position, dir);
            if (!Physics.SphereCast(ray, 0.05f,out searchHit, detectionRange * 2, LayerMask.GetMask("Wall")))
            {
                Vector3 temp = dir / Mathf.Pow(shortestDistToObst, 2);
                //Debug.Log(shortestDistToObst);
                //Debug.Log(temp.magnitude);
                return temp;
            }
            shortestDistToObst = Mathf.Min(shortestDistToObst, searchHit.distance);
        }
        return -transform.forward;
    }
}
