using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOID : MonoBehaviour
{
    Vector3 acceleration;

    new Rigidbody rigidbody;

    List<BOID> relevantBoids;
    [Header("Manuverability")]
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float radius = 1f;
    float detectionRangeSqrd;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] [Range(0, 1)] float turningRate = 0.1f;
    [SerializeField] [Range(0, 1)] float detectionView = 0.30f;
    [SerializeField] [Range(0, 360)] float aimTurnRate;
    float detectDotCompare;

    [Header("Weights")]
    [SerializeField] float seperationWeight = 1f;
    [SerializeField] float allignmentWeight = 1f;
    [SerializeField] float cohesionWeight = 1f;
    [SerializeField] float avoidWeight = 10f;

    [Header("RandomWiggle")]
    [SerializeField] float wiggleWeight = 1;
    [SerializeField] float wiggleOffset = 1;
    [SerializeField] float wiggleAmplitude = 1;

    [SerializeField] float targetWeight = 1;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        relevantBoids = new List<BOID>();
        detectionRangeSqrd = Mathf.Pow(detectionRange, 2);
        BoidManager.allBoids.Add(this);
    }
    private void OnDestroy()
    {
        BoidManager.allBoids.Remove(this);
    }

    internal void UpdateMovement(Vector3 targetDir)
    {
        Detect();
        acceleration = CalcAcceleration();
        acceleration += AvoidObstacle();
        acceleration += RandomWiggle() * wiggleWeight;
        acceleration += targetDir * targetWeight;
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, rigidbody.velocity + acceleration, turningRate);
        if (rigidbody.velocity.sqrMagnitude > Mathf.Pow(maxSpeed, 2))
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        else if (rigidbody.velocity.sqrMagnitude < Mathf.Pow(maxSpeed / 10f, 2))
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed / 10f;
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
    }

    internal void Stop()
    {
        rigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Used to turn enemy without movement
    /// </summary>
    /// <param name="target"></param>
    internal void TurnTo(Vector3 target)
    {
        Transform tempTrans = transform;
        tempTrans.LookAt(target);
        rigidbody.MoveRotation(tempTrans.rotation);
    }

    /// <summary>
    /// Might work currently(?)
    /// </summary>
    /// <param name="target"></param>
    internal void TurnGradual(Vector3 target)
    {
        Transform tempTransform = transform;
        Vector3 targetDirection = target - transform.position;
        targetDirection.Normalize();
        Quaternion temp = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, temp, aimTurnRate * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, detectionRange*2);
    }

    private void Detect()
    {
        detectDotCompare = -(detectionView * 2 - 1);
        relevantBoids.Clear();
        foreach (BOID other in BoidManager.allBoids)
        {
            if (other == this)
                continue;
            Vector3 toOther = other.transform.position - transform.position;
            Vector3 normalizedToOther = Vector3.Normalize(toOther);
            if (toOther.sqrMagnitude < detectionRangeSqrd)
                if (Vector3.Dot(transform.forward, normalizedToOther) > detectDotCompare)
                    relevantBoids.Add(other);
        }
    }
    /// <summary>
    /// Calculates the acceleration vector from BOID behavior
    /// </summary>
    /// <returns>Acceleration</returns>
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
    /// <summary>
    /// Gets a vector pointing away from a visible boid entity
    /// </summary>
    /// <param name="other"></param>
    /// <returns>seperation vector</returns>
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

    /// <summary>
    /// Finds an unobstructed directon if risking collision
    /// </summary>
    /// <returns></returns>
    private Vector3 AvoidObstacle()
    {
        RaycastHit hit;
        if (!Physics.SphereCast(transform.position, radius, transform.forward, out hit, detectionRange * 2, LayerMask.GetMask("Wall")))
            return Vector3.zero;
        Vector3[] obstAvoidRayDirs = BoidManager.CollisionRayDirections;
        float shortestDistToObst = float.MaxValue;
        for (int i = 0; i < obstAvoidRayDirs.Length; i++)
        {
            RaycastHit searchHit;
            Vector3 dir = transform.TransformDirection(obstAvoidRayDirs[i]);
            Ray ray = new Ray(transform.position, dir);
            if (!Physics.SphereCast(ray, radius, out searchHit, detectionRange * 2, LayerMask.GetMask("Wall")))
            {
                Vector3 avoidVec = dir * avoidWeight / Mathf.Pow(shortestDistToObst, 2);
                return avoidVec;
            }
            shortestDistToObst = Mathf.Min(shortestDistToObst, searchHit.distance); //sets shortest distance to obstacle, used to determin magnintude of avoid vector
        }
        return Vector3.zero;
    }
    /// <summary>
    /// Gives a random jitter to prevent wall-hugging
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomWiggle()
    {
        return Vector3.Normalize(transform.forward * wiggleOffset + UnityEngine.Random.insideUnitSphere * wiggleAmplitude);
    }
}
