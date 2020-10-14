using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement settings")]
    [Tooltip("This is the standard thrust force used to move the player.")]
    [SerializeField] private float standardthrustForce;
    [Tooltip("This is the additional force that will be added to the standard thrust when the player is boosting their movement.")]
    [SerializeField] private float additionalBoostForce;
    [SerializeField] private float maxRollRate = 5.0f;
    [SerializeField] private float rollLerpTime = 2f;
    private float thrustForce;
    private float currentRollRate = 0.0f;
    private float acceleration;
    private float dragCoef;
    private float currentMaxSpeed;
    [Tooltip("The max speed that the player can travel in when not boosting.")]
    [SerializeField] private float maxNormalSpeed;
    [Tooltip("The new max speed that the player can travel when actively boosting.")]
    [SerializeField] private float maxSprintSpeed;
    private float stopSpeed = -2f;

    [Header("Mouse look Settings")]
    [Tooltip("Do you want to invert the direction for looking up and down with the mouse?")]
    [SerializeField] private bool invertY = false;
    [SerializeField, Range(.1f, 2f)] private float mouseSensitivityMultiplier = 1f; //Might move this later to a manager type script
    /// <summary>
    /// CameraState class from the unity base camera script, slightly changed. Handles mouse look.
    /// </summary>
    class CameraState
    {
        public float yaw;
        public float pitch;
        public float roll;
        public float x;
        public float y;
        public float z;

        public void SetFromTransform(Transform t)
        {
            pitch = t.eulerAngles.x;
            yaw = t.eulerAngles.y;
            roll = t.eulerAngles.z;
            x = t.position.x;
            y = t.position.y;
            z = t.position.z;
        }

        public void Translate(Vector3 translation)
        {
            Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

            x += rotatedTranslation.x;
            y += rotatedTranslation.y;
            z += rotatedTranslation.z;
        }

        public void UpdateRotation(CameraState target, Transform t)
        {
            //t.eulerAngles = new Vector3(pitch, yaw, roll);
            t.Rotate(new Vector3(target.pitch, target.yaw, target.roll), Space.Self);
        }
    }

    private CameraState m_TargetCameraState = new CameraState();
    private CameraState m_CameraState = new CameraState();

    /// <summary>
    /// Gets the players desired direction of movement.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        direction += gameObject.transform.right * Input.GetAxis("Left-Right");
        direction += gameObject.transform.up * Input.GetAxis("Up-Down");
        direction += gameObject.transform.forward * Input.GetAxis("Forward-Back");
        return direction;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        dragCoef = rb.drag;
    }

    private void OnEnable()
    {
        m_TargetCameraState.SetFromTransform(transform);
        m_CameraState.SetFromTransform(transform);
    }

    /// <summary>
    /// Update here mostly handles rotation (mouse look) of the player as well and the roll rate.
    /// </summary>
    private void Update()
    {
        Vector3 direction = Vector3.zero;

        #if ENABLE_LEGACY_INPUT_MANAGER

        // Mouse Rotation
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));
        m_TargetCameraState.yaw = mouseMovement.x * mouseSensitivityMultiplier;
        m_TargetCameraState.pitch = mouseMovement.y * mouseSensitivityMultiplier;

        //Keyboard rotation
        if(Input.GetAxis("Roll") == 0)
        {
            //Smoothly stops the rotation
            currentRollRate = Mathf.Lerp(currentRollRate, 0, 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / (rollLerpTime)) * Time.deltaTime));
            transform.Rotate(new Vector3(0, 0, currentRollRate));
        }
        else
        {
            currentRollRate = Mathf.Lerp(currentRollRate, maxRollRate * Input.GetAxis("Roll"), 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rollLerpTime) * Time.deltaTime));
            transform.Rotate(new Vector3(0, 0, currentRollRate));
        }

        //Sprint/Boost
        if (Input.GetButton("Sprint"))
        {
            Debug.Log("Why?");
            thrustForce = standardthrustForce + additionalBoostForce;
            Debug.Log("Hello?");
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSprintSpeed);
            Debug.Log("HELLO I AM SETTING THE CURRENTMAXSPEED TO MAXSPRINTSPEED FOR SOME FUCKING REASON");
            currentMaxSpeed = maxSprintSpeed;
        }
        else
        {
            thrustForce = standardthrustForce;
            if (rb.velocity.magnitude > maxNormalSpeed + .5f)
            {
                currentMaxSpeed = Mathf.Lerp(currentMaxSpeed, maxNormalSpeed, 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / stopSpeed) * Time.deltaTime));
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, currentMaxSpeed);
            }
            else
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxNormalSpeed);
            }

        }

        // Translation
        direction = GetInputTranslationDirection() * Time.deltaTime;
        #elif USE_INPUT_SYSTEM
            // TODO: make the new input system work
        #endif

        m_TargetCameraState.Translate(direction);
        m_CameraState.UpdateRotation(m_TargetCameraState, transform);

        ////Keyboard Movement
        //direction.Normalize();
        //acceleration = thrustForce / rb.mass;
        //acceleration *= Time.deltaTime;
        //velocity += acceleration * direction;
        //Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //newPos += velocity * Time.deltaTime;
        //rb.MovePosition(newPos);

        //if (direction.x == 0)
        //{
        //    Mathf.SmoothDamp(velocity.x, 0f, ref velocity.x, 1f);
        //}
        //if (direction.y == 0)
        //{
        //    Mathf.SmoothDamp(velocity.y, 0f, ref velocity.y, 1f);
        //}
        //if (direction.z == 0)
        //{
        //    Mathf.SmoothDamp(velocity.z, 0f, ref velocity.z, 1f);
        //}

    }


    /// <summary>
    /// Everything that uses physics or force should be added here. Handles the movement of the player by adding force to the rigidbody.
    /// Right now uses drag to simulate stopping.
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 translation = Vector3.zero;
        translation = GetInputTranslationDirection();

        //Keyboard movement and thrust
        if (translation != Vector3.zero)
        {
            Vector3 direction = translation.normalized;
            direction *= thrustForce;
            rb.AddForce(direction, ForceMode.Force);
            rb.drag = 0;
        }
        else rb.drag = dragCoef;

        //Doesn't wörk, might use later for now, use rigidbody drag to achieve similar effect
        //else
        //{
        //    var velocityLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.8f) / stopLerpTime) * Time.deltaTime);
        //    Vector3 velocityVector = rb.velocity.normalized;
        //    Vector3 dampeningVector = -velocityVector;
        //    rb.AddForce(dampeningVector * dampeningThrustForce, ForceMode.Force);
        //}
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 60), "Speed limit: " + currentMaxSpeed.ToString());
        GUI.Label(new Rect(10, 30, 200, 60), "Roll rate: " + currentRollRate.ToString());
        GUI.Label(new Rect(10, 50, 200, 60), "rb.Velocity.magnitude: " + rb.velocity.magnitude.ToString());
    }
}
