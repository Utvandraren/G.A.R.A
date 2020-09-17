using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float maxSpeed; //Will use later hopefully

    //Rigidbody and physics related
    private Rigidbody rb;
    [SerializeField] private float thrustForce;
    [SerializeField] private float dampeningThrustForceMax;
    private float dampeningThrustForce;
    private float dragCoef = .8f;
    private float angularDragCoef = 10;

    float currentRollRate = 0.0f;
    float maxRollRate = 5.0f;
    [SerializeField] private float rollLerpTime = 2f;

    [SerializeField, Range(.1f, 2f)] private float mouseSensitivityMultiplier = 1f; //Might move this later to a manager type script

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

    [Header("Movement Settings")]
    [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
    public bool invertY = false;

    private Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += gameObject.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += -gameObject.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += -gameObject.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += gameObject.transform.right;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            direction += -gameObject.transform.up;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            direction += gameObject.transform.up;
        }
        return direction;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.drag = dragCoef;
        rb.angularDrag = angularDragCoef;
        dampeningThrustForce = dampeningThrustForceMax;
    }

    private void OnEnable()
    {
        m_TargetCameraState.SetFromTransform(transform);
        m_CameraState.SetFromTransform(transform);
    }

    private void Update()
    {
        Vector3 translation = Vector3.zero;

        #if ENABLE_LEGACY_INPUT_MANAGER

        // Mouse Rotation
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));
        m_TargetCameraState.yaw = mouseMovement.x * mouseSensitivityMultiplier;
        m_TargetCameraState.pitch = mouseMovement.y * mouseSensitivityMultiplier;

        //Keyboard rotation
        if (Input.GetKey(KeyCode.Q))
        {
            currentRollRate = Mathf.Lerp(currentRollRate, maxRollRate, 1f - Mathf.Exp((Mathf.Log(1f - 0.8f) / rollLerpTime) * Time.deltaTime));
            transform.Rotate(new Vector3(0, 0, currentRollRate));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentRollRate = Mathf.Lerp(currentRollRate, -maxRollRate, 1f - Mathf.Exp((Mathf.Log(1f - 0.8f) / rollLerpTime) * Time.deltaTime));
            transform.Rotate(new Vector3(0, 0, currentRollRate));
        }
        else
        {
            currentRollRate = Mathf.Lerp(currentRollRate, 0, 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / (rollLerpTime)) * Time.deltaTime));
            transform.Rotate(new Vector3(0, 0, currentRollRate));
        }


        // Translation
        translation = GetInputTranslationDirection() * Time.deltaTime;
        #elif USE_INPUT_SYSTEM
            // TODO: make the new input system work
        #endif

        m_TargetCameraState.Translate(translation);
        m_CameraState.UpdateRotation(m_TargetCameraState, transform);
    }


    //Everything physics related must be added here, not in Update()
    private void FixedUpdate()
    {
        Vector3 translation = Vector3.zero;
        translation = GetInputTranslationDirection() * Time.deltaTime;

        //Movement and Thrust
        if (translation != Vector3.zero)
        {
            Vector3 direction = translation.normalized;
            direction *= thrustForce;
            rb.AddForce(direction * Time.deltaTime, ForceMode.Force);
            dampeningThrustForce = dampeningThrustForceMax;
        }

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
        GUI.Label(new Rect(10, 10, 250, 100), currentRollRate.ToString());
    }
}
