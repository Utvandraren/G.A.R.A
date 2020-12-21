using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxForce = 100f;

    private Rigidbody rb;
    private Vector3 forceToAdd;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {      
        forceToAdd = (target.position - transform.position) * speed * Time.deltaTime;
        rb.AddForce(forceToAdd);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxForce);
    }

    void OnDestroy()
    {
        GetComponent<EnemyDrops>().DropAmmo(transform.position);
    }
}
