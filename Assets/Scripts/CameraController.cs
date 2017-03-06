using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CameraController
{
    void Reset();
    void GetNextState(out Vector3 position, out Quaternion rotation);
}