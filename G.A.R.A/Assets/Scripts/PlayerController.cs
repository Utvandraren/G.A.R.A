using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float maxSpeed;

    [SerializeField] private float thrustForce;
    [SerializeField] private float stoppingThrustForce;

    [SerializeField, Range(.1f, 2f)] private float mouseSensitivityMultiplier = 1f;

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

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }

            public void UpdateTransform(CameraState target, Transform t)
            {
                //t.eulerAngles = new Vector3(pitch, yaw, roll);
                t.Rotate(new Vector3(target.pitch, target.yaw, target.roll), Space.Self);
                t.position = new Vector3(x, y, z);
            }
        }

        CameraState m_TargetCameraState = new CameraState();
        CameraState m_InterpolatingCameraState = new CameraState();

        [Header("Movement Settings")]
        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        public bool invertY = false;

        void OnEnable()
        {
            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);
        }

        Vector3 GetInputTranslationDirection()
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
            if (Input.GetKey(KeyCode.Q))
            {
                direction += Vector3.down;
            }
            if (Input.GetKey(KeyCode.E))
            {
                direction += Vector3.up;
            }
            return direction;
        }

    void Update()
    {
        Vector3 translation = Vector3.zero;

        #if ENABLE_LEGACY_INPUT_MANAGER


        // Rotation

        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

        m_TargetCameraState.yaw = mouseMovement.x * mouseSensitivityMultiplier;
        m_TargetCameraState.pitch = mouseMovement.y * mouseSensitivityMultiplier;

        // Translation
        translation = GetInputTranslationDirection() * Time.deltaTime;

        #elif USE_INPUT_SYSTEM
            // TODO: make the new input system work
        #endif

        m_TargetCameraState.Translate(translation);

        //// Framerate-independent interpolation
        //// Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
        //var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
        //var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
        //m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

        m_InterpolatingCameraState.UpdateTransform(m_TargetCameraState, transform);
    }
}
