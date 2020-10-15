using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOID : MonoBehaviour
{
    AIMoveEngine engine;
    Vector3 direction;

    new Rigidbody rigidbody;
    List<BOID> relevantBoids;
    [Header("Detection")]
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float radius = 1f;
    [SerializeField] [Range(0, 1)] float detectionView = 0.30f;
    float detectionRangeSqrd;
    float detectDotCompare;

    [Header("Weights")]
    [SerializeField] float seperationWeight = 1f;
    [SerializeField] float allignmentWeight = 1f;
    [SerializeField] float cohesionWeight = 1f;
    [SerializeField] float avoidWeight = 10f;
    [SerializeField] float targetWeight = 1;
    [SerializeField] float wiggleWeight = 1;

    [Header("RandomWiggle")]//might not want this here
    [SerializeField] float wiggleOffset = 1;
    [SerializeField] float wiggleAmplitude = 1;

    // Start is called before the first frame update
    void Start()
    {
        engine = GetComponent<AIMoveEngine>();
        rigidbody = GetComponent<Rigidbody>();
        relevantBoids = new List<BOID>();
        detectionRangeSqrd = Mathf.Pow(detectionRange, 2);
        BoidManager.allBoids.Add(this);
    }

    public void OnDestroy()
    {
        BoidManager.allBoids.Remove(this);
    }

    internal void UpdateMovement(Vector3 targetDir)
    {
        Detect();
        direction = CalculateDirection();
        direction += AvoidObstacle();
        direction += RandomWiggle() * wiggleWeight;
        direction += targetDir * targetWeight;
        direction.Normalize();
        engine.DirectionRotateAndMove(direction);
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
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
    private Vector3 CalculateDirection()
    {
        Vector3 seperationVector = Vector3.zero;
        Vector3 allignmentVector = Vector3.zero;
        Vector3 cohesionVector = Vector3.zero;

        foreach (BOID other in relevantBoids)
        {
            seperationVector += CalculateSeperation(other);
            allignmentVector += CalculateAlignment(other);
            cohesionVector += CalculateCohesion(other);
        }
        Vector3 tempDirection;
        if (relevantBoids.Count > 0)
            tempDirection = (seperationVector * seperationWeight +
                                allignmentVector * allignmentWeight +
                                cohesionVector * cohesionWeight) /
                                relevantBoids.Count;
        else
            tempDirection = transform.forward;
        return tempDirection;
    }
    /// <summary>
    /// Gets a vector pointing away from a visible boid entity
    /// </summary>
    /// <param name="other"></param>
    /// <returns>seperation vector</returns>
    private Vector3 CalculateSeperation(BOID other)
    {
        Vector3 awayVector = -(other.transform.position - transform.position);
        awayVector /= (awayVector.sqrMagnitude);
        return awayVector;
    }
    private Vector3 CalculateAlignment(BOID other)
    {
        return other.rigidbody.velocity;
    }
    private Vector3 CalculateCohesion(BOID other)
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
        if (!Physics.SphereCast(transform.position, radius * 2, transform.forward, out hit, detectionRange * 2, LayerMask.GetMask("Wall")))
            return Vector3.zero;
        Vector3[] obstAvoidRayDirs = BoidManager.CollisionRayDirections;
        float shortestDistToObst = float.MaxValue;
        for (int i = 0; i < obstAvoidRayDirs.Length; i++)
        {
            RaycastHit searchHit;
            Vector3 dir = transform.TransformDirection(obstAvoidRayDirs[i]);
            Ray ray = new Ray(transform.position, dir);
            if (!Physics.SphereCast(ray, radius * 2, out searchHit, detectionRange * 2, LayerMask.GetMask("Wall")))
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
