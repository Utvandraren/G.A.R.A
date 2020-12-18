using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float turningSpeed = 0f; //Original 35

    private GameObject target;
    private Transform currentTransform;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 currentDirection = target.transform.position - transform.position;

        //transform.forward = Vector3.Lerp(target.transform.position, transform.position, 1).normalized;
        
        Quaternion neededRotation = Quaternion.LookRotation(currentDirection);
        
        //transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * 2f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * turningSpeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, neededRotation, 0.5f);
    }
}
