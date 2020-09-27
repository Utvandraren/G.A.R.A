using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A test class to show the activation_interaction
/// </summary>
public class Test_ActivationInteract : MonoBehaviour
{
    private float r = 0f;
    private float g = 0f;
    private float b = 0f;

    Color lerpedColor = Color.white;

    public void Update()
    {
        /*
         * When the object is active the object gradually changes color.
         * */
        if(gameObject.GetComponent<ActivateInteraction>().active == ActivationEnum.On)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", lerpedColor);
            lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));

            //r += 0.004f;
            //b += 0.002f;
            //g += 0.001f;
            //if(r >= 255)
            //{
            //    r = 3;
            //}
            //if (g >= 255)
            //{
            //    g = 2;
            //}
            //if (b >= 255)
            //{
            //    b = 1;
            //}
        }
    }

    
}
