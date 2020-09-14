using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public static Vector3[] CollisionRayDirections { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        GetSampleDirections();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns well spaced directions on a sphere
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
}
