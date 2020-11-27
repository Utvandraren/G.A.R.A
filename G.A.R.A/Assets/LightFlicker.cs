using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float minIncrease = -1f;
    [SerializeField] private float maxIncrease = 1f;

    private float minIntensity = 0f;
    private float MaxIntensity = 100f;
    private Light flickerLight;

    // Start is called before the first frame update
    void Start()
    {
        flickerLight = GetComponent<Light>();
        MaxIntensity = flickerLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {

        flickerLight.intensity += (Random.Range(minIncrease, maxIncrease));
        flickerLight.intensity = Mathf.Clamp(flickerLight.intensity, minIntensity, MaxIntensity);
    }
}
