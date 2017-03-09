using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public KeyboardMouseConfig keyboardMouseConfig;
    public float horizontalDirection;
    public float verticalDirection;
    public bool holdingJump;
    public bool jumped;
    public bool targeted;
    public bool teleported;
    public bool shooting;
    public Vector3 rotation;

    void Update()
    {
        this.SetDirection();
        this.SetRotation();
        this.SetJump();
        this.SetTargeting();
        this.SetTeleport();
        this.SetShoot();
    }

    void SetDirection()
    {
        this.horizontalDirection = 0;
        if (Input.GetKey(this.keyboardMouseConfig.left))
        {
            this.horizontalDirection = -1;
        }
        else if (Input.GetKey(this.keyboardMouseConfig.right))
        {
            this.horizontalDirection = 1;
        }
        this.verticalDirection = 0;
        if (Input.GetKey(this.keyboardMouseConfig.down))
        {
            this.verticalDirection = -1;
        }
        else if (Input.GetKey(this.keyboardMouseConfig.up))
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
        jumped = Input.GetKeyDown(this.keyboardMouseConfig.jump);
        holdingJump = Input.GetKey(this.keyboardMouseConfig.jump);
    }

    void SetTargeting()
    {
        targeted = Input.GetKeyDown(this.keyboardMouseConfig.target);
    }

    void SetTeleport()
    {
        teleported = Input.GetKeyDown(this.keyboardMouseConfig.teleport);
    }

    void SetShoot()
    {
        shooting = Input.GetKey(this.keyboardMouseConfig.shoot);
    }
}