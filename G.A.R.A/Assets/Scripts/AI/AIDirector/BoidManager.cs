using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds the list of boid elements and manages removal from the list
/// Holds CollisionRayDirections for every boid to use as reference
/// </summary>
public class BoidManager : MonoBehaviour
{
    public static Vector3[] CollisionRayDirections { get; private set; }
    public static List<BOID> allBoids;
    private static Director director;
    // Start is called before the first frame update
    void Awake()
    {
        GetSampleDirections();
        allBoids = new List<BOID>();
        director = GetComponent<Director>();
    }

    /// <summary>
    /// Calculates well spaced directions on a sphere
    /// Used to query from a point in front to a point in the back sequentially
    /// </summary>
    /// <param name="numSamples"></param>
    /// <returns></returns>
    // Credit to Sebastian Lague from programming adventures fame
    public static void GetSampleDirections(int numSamples = 300)
    {
        int numPoints = numSamples;
        CollisionRayDirections = new Vector3[numPoints];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numPoints; i++)
        {
            float directionFraction = (float)i / numPoints;
            float inclination = Mathf.Acos(1 - 2 * directionFraction);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            CollisionRayDirections[i] = new Vector3(x, y, z);
        }
    }

    public static void DestroyBoid(BOID boid)
    {
        Vector3 deathPos = new Vector3(boid.transform.position.x, boid.transform.position.y, boid.transform.position.z);
        if(director != null)
            director.EnemyIsDestroyed(deathPos);
        allBoids.Remove(boid);
    }
}
