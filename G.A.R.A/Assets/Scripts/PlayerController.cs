using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float maxSpeed;

    [SerializeField] private float thrustForce;
    [SerializeField] private float stoppingThrustForce;

    [SerializeField, Range(1f, 1000f)] private float mouseSensitivityMultiplier = 10;

    private float yaw;
    private float pitch;
    private float roll;
    private float x;
    private float y;
    private float z;

    private float m_Speed;
    public bool m_WorldSpace;

    // Start is called before the first frame update
    void Start()
    {
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
        roll = transform.eulerAngles.z;
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    //public void UpdateTransform(Transform t)
    //{
    //    t.Rotate(new Vector3(pitch, yaw, roll),)
    //    t.position = new Vector3(x, y, z);
    //}

    Vector3 GetInputDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            direction += Vector3.up;
        }
        return direction;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        pitch = -mouseMovement.y;
        yaw = mouseMovement.x;

        UpdateTransform(transform);
    }

    private void UpdateTransform(Transform t)
    {
        t.Rotate(new Vector3(pitch, yaw, roll) * mouseSensitivityMultiplier * Time.deltaTime, Space.Self);
        //Add result of forces to velocity and direction, remember velocity direction is one thing, thrust direction is another
    }
}
