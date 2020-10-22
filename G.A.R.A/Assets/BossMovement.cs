using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float turningSpeed = 0f;

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
        Quaternion neededRotation = Quaternion.LookRotation(currentDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * turningSpeed);
    }
}
