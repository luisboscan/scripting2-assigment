using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CameraController
{
    /// <summary>
    /// Updates the position and rotation of the camera
    /// </summary>
    void UpdateCameraState();

    /// <summary>
    /// Resets the position and rotation of the camera to the 
    /// inital state before being manipulated by this controler,
    /// if necesary.
    /// </summary>
    void Reset();

    /// <summary>
    /// Returns the next desired state (position and rotation) of the camera
    /// </summary>
    void GetNextState(out Vector3 position, out Quaternion rotation);
}