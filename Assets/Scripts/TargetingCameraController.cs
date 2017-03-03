using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCameraController : MonoBehaviour {

    public GameObject player;
    public GameObject target;
    public float distanceBehindPlayer = 4f;
    public float angle = 45f;
    public float smoothTime = 0.2f;

    private Vector3 currentDampVelocity;
    private Vector3 currentDampVelocity2;

    void FixedUpdate () {
        Vector3 focusPoint = GetFocusPoint();
        transform.LookAt(focusPoint);
        transform.position = Vector3.SmoothDamp(transform.position, GetNextPosition(focusPoint), ref currentDampVelocity, smoothTime);
    }

    private Vector3 GetFocusPoint()
    {
        // The focus point is the middle position between the target and the player
        return player.transform.position + (target.transform.position - player.transform.position);
    }

    public Vector3 GetNextPosition()
    {
        Vector3 focusPoint = GetFocusPoint();
        return GetNextPosition(focusPoint);
    }

    public void GetNextPosition(out Vector3 nextPosition, out Quaternion nextRotation)
    {
        Vector3 focusPoint = GetFocusPoint();
        nextPosition = GetNextPosition(focusPoint);
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        transform.position = nextPosition;
        transform.LookAt(focusPoint);
        nextRotation = transform.rotation;
        transform.position = currentPosition;
        transform.rotation = currentRotation;
    }

    private Vector3 GetNextPosition(Vector3 focusPoint)
    {
        Vector3 directionBehindPlayer = Vector3.Normalize(player.transform.position - focusPoint);
        // Move the camera vertically depending on the specified angle
        directionBehindPlayer.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 nextPosition = player.transform.position + (directionBehindPlayer * distanceBehindPlayer);
        return nextPosition;
    }
}
