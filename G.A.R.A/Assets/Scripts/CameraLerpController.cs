using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpController : MonoBehaviour
{

    //The point of this script is to lerp to the new camera position, 
    //because Unity caused slighly jittery movement when only using rigidbody interpolation
    private Transform target;
    private float targetPosX;
    private float targetPosY;
    private float targetPosZ;

    private float targetRotX;
    private float targetRotY;
    private float targetRotZ;

    private float lerpTimePcts = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("CameraTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation lerp
        targetRotZ = target.transform.rotation.z;
        float tempRotZ = Mathf.Lerp(transform.rotation.z, targetRotZ, 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / (lerpTimePcts)) * Time.deltaTime));
        Quaternion newRotation = new Quaternion(target.transform.rotation.x, target.transform.rotation.y, tempRotZ, target.transform.rotation.w);
        transform.rotation = newRotation;


        //Position lerp
        float tempPosX;
        float tempPosY;
        float tempPosZ;

        targetPosX = target.position.x;
        targetPosY = target.position.y;
        targetPosZ = target.position.z;

        tempPosX = Mathf.Lerp(transform.position.x, targetPosX, 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / (lerpTimePcts)) * Time.deltaTime));
        tempPosY = Mathf.Lerp(transform.position.y, targetPosY, 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / (lerpTimePcts)) * Time.deltaTime));
        tempPosZ = Mathf.Lerp(transform.position.z, targetPosZ, 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / (lerpTimePcts)) * Time.deltaTime));

        transform.position = new Vector3(tempPosX, tempPosY, tempPosZ);
    }
}
