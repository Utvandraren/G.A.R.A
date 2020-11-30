using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{

    [SerializeField, Range(0F, 1F)] private float revolveFactor;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * revolveFactor);
    }
}
