using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public KeyboardMouseConfig keyboardMouseConfig;
    public WindowsGamepadConfig windowsGamepadConfig;
    public float horizontalDirection;
    public float verticalDirection;
    public bool holdingJump;
    public bool jumped;

    void Update()
    {
        this.SetDirection();
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

    void SetJump()
    {
        jumped = Input.GetKeyDown(this.keyboardMouseConfig.jump) || Input.GetKeyDown(this.windowsGamepadConfig.jump);
        holdingJump = Input.GetKey(this.keyboardMouseConfig.jump) || Input.GetKey(this.windowsGamepadConfig.jump);
    }
}