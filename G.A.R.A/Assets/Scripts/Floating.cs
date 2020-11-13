

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private Vector3 randomForce;
    private Rigidbody rigidbody;
    public float randomRange;
    bool first = true;


    // Start is called before the first frame update
    void Start()
    {
        if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rigidbody = rb;
        }

    }
    private void FixedUpdate()
    {
        if (first)
        {
            float x = Random.Range(-randomRange, randomRange);
            float y = Random.Range(-randomRange, randomRange);
            float z = Random.Range(-randomRange, randomRange);
            first = false;
            randomForce = new Vector3(x, y, z);
        }
        rigidbody.AddForce(randomForce);
    }
}
