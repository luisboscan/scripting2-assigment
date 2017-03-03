using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject target;
    public GameObject dummy;
    public float rotationSpeed;
    Vector3 offset;
    float currentAngle;
    Quaternion rotation;
    public float smoothTime = 0.1f;

    private Vector3 currentDampVelocity;
    private Vector3 currentDampVelocity2;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = target.transform.position - transform.position;
        rotation = transform.rotation;
    }

    void FixedUpdate()
    {
        float horizontal = playerInput.rotation.x;//Input.GetAxis("Mouse X") * rotationSpeed;

        currentAngle = currentAngle + horizontal * Time.fixedDeltaTime;

        Vector3 nextPosition;
        Quaternion nextRotation;
        GetNextPosition(out nextPosition, out nextRotation);

        transform.position = nextPosition;
        transform.rotation = nextRotation;
    }

    public void GetNextPosition(out Vector3 nextPosition, out Quaternion nextRotation)
    {
        dummy.transform.position = target.transform.position - offset;
        dummy.transform.rotation = rotation;
        dummy.transform.RotateAround(target.transform.position, Vector3.up, currentAngle);
        nextPosition = dummy.transform.position;
        nextRotation = dummy.transform.rotation;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}