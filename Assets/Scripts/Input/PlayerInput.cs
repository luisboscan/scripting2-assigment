using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public KeyboardMouseConfig keyboardMouseConfig;
    public WindowsGamepadConfig windowsGamepadConfig;
    public float horizontalDirection;
    public float verticalDirection;
    public bool holdingJump;
    public bool jumped;
    [Tooltip("Rotation from the mouse to apply on the camera.")]
    public Vector3 rotation;

    void Update()
    {
        this.SetDirection();
        this.SetRotation();
        this.SetJump();
    }

    void SetDirection()
    {
        this.horizontalDirection = 0;
        if (Input.GetKey(this.keyboardMouseConfig.left) || Input.GetAxisRaw("Horizontal") < 0)
        {
            this.horizontalDirection = -1;
        }
        else if (Input.GetKey(this.keyboardMouseConfig.right) || Input.GetAxisRaw("Horizontal") > 0)
        {
            this.horizontalDirection = 1;
        }
        this.verticalDirection = 0;
        if (Input.GetKey(this.keyboardMouseConfig.down) || Input.GetAxisRaw("Vertical") < 0)
        {
            this.verticalDirection = -1;
        }
        else if (Input.GetKey(this.keyboardMouseConfig.up) || Input.GetAxisRaw("Vertical") > 0)
        {
            this.verticalDirection = 1;
        }
    }

    private void SetRotation()
    {
        float yaw = Input.GetAxis("Mouse X") * this.keyboardMouseConfig.mouseXSensitivity;
        float pitch = Input.GetAxis("Mouse Y") * this.keyboardMouseConfig.mouseYSensitivity;

        if (this.keyboardMouseConfig.invertY)
        {
            pitch *= -1;
        }

        this.rotation = new Vector3(yaw, pitch, 0f);
    }

    void SetJump()
    {
        jumped = Input.GetKeyDown(this.keyboardMouseConfig.jump) || Input.GetKeyDown(this.windowsGamepadConfig.jump);
        holdingJump = Input.GetKey(this.keyboardMouseConfig.jump) || Input.GetKey(this.windowsGamepadConfig.jump);
    }
}