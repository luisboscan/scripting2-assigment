using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour, CameraController
{
    public Camera cameraComponent;
    public PlayerInput playerInput;
    public GameObject target;

    private Vector3 offsetBetweenTargetAndCamera;
    private Quaternion originalCameraRotation;
    private float currentHorizontalAngle;
    private GameObject dummy;

    void Start()
    {
        offsetBetweenTargetAndCamera = target.transform.position - cameraComponent.transform.position;
        originalCameraRotation = cameraComponent.transform.rotation;
        dummy = new GameObject("Camera Helper");
    }

    public void UpdateCameraState()
    {
        currentHorizontalAngle = currentHorizontalAngle + playerInput.rotation.x * Time.fixedDeltaTime;

        Vector3 nextPosition;
        Quaternion nextRotation;
        GetNextState(out nextPosition, out nextRotation);

        cameraComponent.transform.position = nextPosition;
        cameraComponent.transform.rotation = nextRotation;
    }

    public void GetNextState(out Vector3 position, out Quaternion rotation)
    {
        dummy.transform.position = target.transform.position - offsetBetweenTargetAndCamera;
        dummy.transform.rotation = originalCameraRotation;
        dummy.transform.RotateAround(target.transform.position, Vector3.up, currentHorizontalAngle);
        position = dummy.transform.position;
        rotation = dummy.transform.rotation;
    }

    public void Reset()
    {
        currentHorizontalAngle = cameraComponent.transform.rotation.eulerAngles.y;
    }
}