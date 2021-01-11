using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// This script handles all the movement the player can perform.
/// It includes mouse and keyboard movement. 
/// In order to function, the object it is attached to should have a rigidbody, a collider and a low-friction physics material assigned to it.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement settings")]
    [Tooltip("This is the standard thrust force used to move the player.")]
    [SerializeField] private float standardthrustForce;
    [Tooltip("This is the additional force that will be added to the standard thrust when the player is boosting their movement.")]
    [SerializeField] private float additionalBoostForce;
    [SerializeField] private float maxRollRate;
    [SerializeField] private float rollLerpTime;
    [Tooltip("This is the time it takes to slow down to normal speed after releasing the sprint button.")]
    [SerializeField] private float stopLerpTime;
    [Tooltip("The max speed that the player can travel in when not boosting.")]
    [SerializeField] private float maxNormalSpeed;
    [Tooltip("The new max speed that the player can travel when actively boosting.")]
    [SerializeField] private float maxSprintSpeed;
    [SerializeField, Range(0.01f, .99f)] private float stopSmoothFactor;
    [SerializeField, Range(0.01f, .99f)] private float rollSmoothFactor;
    [Tooltip("The amount of drag that will be applied when the player lets go of all movement buttons.")]
    [SerializeField] private float stopDragCoef = 5;

    private float thrustForce;
    private float currentRollRate = 0.0f;
    private float acceleration;
    private float currentMaxSpeed;
    public bool isSprinting { get; private set; }

    private float rollLerpPct;
    private float stopLerpPct;

    private PlayerStats playerStats;

    [Header("Mouse look Settings")]
    [Tooltip("Do you want to invert the direction for looking up and down with the mouse?")]
    [SerializeField] private bool invertY = false;
    private float mouseSensitivityMax = 2f;
    [SerializeField] private float mouseSensitivityMultiplier;
    


    /// <summary>
    /// CameraState class from the unity base camera script, modified. Handles mouse look rotation.
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
        currentMaxSpeed = maxNormalSpeed;
        stopDragCoef = rb.drag;
        playerStats = GetComponent<PlayerStats>();
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

        if (PauseMenu.GameIsPaused)
        {
            return;
        }

#if ENABLE_LEGACY_INPUT_MANAGER

        // Mouse Look Rotation
        HandleMouseLook();

        //Keyboard rotation
        HandleRoll();

        //Sprint/Boost
        HandleMovement();

        //Final direction to start moving towards
        direction = GetInputTranslationDirection() * Time.deltaTime;

        #elif USE_INPUT_SYSTEM
        #endif

        m_TargetCameraState.Translate(direction);
        m_CameraState.UpdateRotation(m_TargetCameraState, transform);
    }


    /// <summary>
    /// Everything that uses physics or force should be added here. Handles the movement of the player by adding force to the rigidbody.
    /// Right now uses drag to simulate stopping.
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        direction = GetInputTranslationDirection();

        //Keyboard movement and thrust
        if (direction != Vector3.zero)
        {
            Vector3 dir = direction.normalized;
            direction *= thrustForce;
            rb.AddForce(direction, ForceMode.Force);
            rb.drag = 0;
        }
        else rb.drag = stopDragCoef;
    }

    private float calculateLerp(float current, float target, float lerpTime, float smoothnessFactor)
    {
        float temp = Mathf.Lerp(current, target, (1f - Mathf.Exp((Mathf.Log(1f - smoothnessFactor) / lerpTime) * Time.deltaTime)));
        return temp;
    }

    public void SetSensitivity(float value)
    {
        mouseSensitivityMultiplier = mouseSensitivityMax * value;
    }

    private void HandleRoll()
    {
        if (Input.GetAxis("Roll") == 0)
        {
            //Smoothly stops the rotation
            currentRollRate = calculateLerp(currentRollRate, 0, rollLerpTime, rollSmoothFactor);
            transform.Rotate(new Vector3(0, 0, currentRollRate));
        }
        else
        {
            currentRollRate = calculateLerp(currentRollRate, maxRollRate * Input.GetAxis("Roll"), rollLerpTime, rollSmoothFactor);
            transform.Rotate(new Vector3(0, 0, currentRollRate));
        }
    }

    private void HandleMovement()
    {
        if (Input.GetButton("Sprint") && playerStats.sprint > 0 && playerStats.canSprint)
        {
            isSprinting = true;
            thrustForce = standardthrustForce + additionalBoostForce;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSprintSpeed);
            currentMaxSpeed = maxSprintSpeed;
        }
        else
        {
            isSprinting = false;
            thrustForce = standardthrustForce;
            
            if (rb.velocity.magnitude > maxNormalSpeed + .5f) //Gradually slow down from sprint max speed to normal max speed
            {
                currentMaxSpeed = calculateLerp(currentMaxSpeed, maxNormalSpeed, stopLerpTime, stopSmoothFactor);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, currentMaxSpeed);
            }
            else //Continue normal movement
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxNormalSpeed);
            }

        }
    }

    private void HandleMouseLook()
    {
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));
        m_TargetCameraState.yaw = mouseMovement.x * mouseSensitivityMultiplier;
        m_TargetCameraState.pitch = mouseMovement.y * mouseSensitivityMultiplier;
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 10, 200, 60), "Speed limit: " + currentMaxSpeed.ToString());
    //    GUI.Label(new Rect(10, 30, 200, 60), "Roll rate: " + currentRollRate.ToString());
    //    GUI.Label(new Rect(10, 50, 200, 60), "rb.Velocity.magnitude: " + rb.velocity.magnitude.ToString());
    //}
}
