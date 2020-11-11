using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script which is only intended for use in the main menu
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField] private float xRotRate;
    [SerializeField] private float yRotRate;
    [SerializeField] private float zRotRate;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotRate * Time.deltaTime, yRotRate * Time.deltaTime, zRotRate * Time.deltaTime);
    }
}
