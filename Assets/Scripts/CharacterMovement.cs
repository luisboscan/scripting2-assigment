using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public CharacterController characterController;
    public GroundCheck groundCheck;
    private Vector3 currentSpeed;
    private Vector3 tmpVector3 = Vector3.zero;

    // Horizontal movement variables
    public float maxGroundSpeed = 0.15f;
    public float groundAcceleration = 0.5f;
    public float groundFriction = 1.2f;
    public float airFriction = 0.2f;
    /// <summary>
    /// True if the player is holding the jump button
    /// </summary>
    private bool holdingJump;

    // Vertical movement variables
    /// <summary>
    /// Max speed that the character can have when falling down, 
    /// represented with positive numbers.
    /// </summary>
    [Tooltip("Max speed that the character can have when falling down, represented with positive numbers.")]
    public float maxFallingSpeed = 0.3f;
    /// <summary>
    /// One time modified given to the vertical 
    /// speed when the jump button is pressed.
    /// </summary>
    [Tooltip("One time modified given to the vertical speed when the jump button is pressed.")]
    public float jumpImpulse = 0.35f;
    public float gravity = 1.4f;
    /// <summary>
    /// Modifier applied to gravity when the character has jumped and 
    /// is ascending, used to make gravity lower so the character spends 
    /// more time going up than going down.
    /// </summary>
    [Tooltip("Modifier applied to gravity when the character has jumped and is ascending, used to make gravity lower so the character spends more time going up than going down.")]
    public float gravityAscensionModifier = 0.7f;
    /// <summary>
    /// Minimum time that the character has to be in the air after jumping, 
    /// used to set a limit for short jumps.
    /// </summary>
    [Tooltip("Minimum time that the character has to be in the air after jumping, used to set a limit for short jumps.")]
    public float minJumpTime = 0.1f;
    /// <summary>
    /// Maximum vertical speed that the player can have when the jump button is 
    /// not being held anymore, only applied after the time in minJumpTime has passed.
    /// </summary>
    [Tooltip("Maximum vertical speed that the player can have when the jump button is not being held anymore, only applied after the time in minJumpTime has passed.")]
    public float cutJumpSpeedLimit = 0.05f;
    /// <summary>
    /// Time given for a character to still be able to jump after 
    /// it has fallen of a ledge.
    /// </summary>
    [Tooltip("Time given for a character to still be able to jump after it has fallen of a ledge.")]
    public float jumpCallTolerance = 0.2f;
    /// <summary>
    /// If it was grounded in the last frame.
    /// </summary>
    private bool wasGrounded;
    private bool jumping;
    private float timeInTheAir = 0f;
    private float timeSinceJumpStarted = 0f;
    private bool cutJumpShort = false;
    private bool jumpStarted;

    private Vector3 currentInput;
    private float currentGroundSpeed;

    public void UpdateInput(float inputX, float inputZ, bool holdingJump)
    {
        currentInput.x = inputX;
        currentInput.z = inputZ;
        currentInput = currentInput.normalized;
        this.holdingJump = holdingJump;
    }

    public void Move()
    {
        bool grounded = groundCheck.IsGrounded;
        Vector3 newSpeed = UpdateSpeed(currentInput, currentSpeed, grounded);
        UpdatePosition(newSpeed);
        currentSpeed = newSpeed;
        ClearInput();
    }

    private void ClearInput()
    {
        UpdateInput(0, 0, false);
    }

    public bool Jump(bool canJumpInMidAir)
    {
        bool grounded = groundCheck.IsGrounded;
        bool canJump = canJumpInMidAir || (!jumping && (grounded || timeInTheAir <= jumpCallTolerance));
        if (canJump)
        {
            cutJumpShort = false;
            jumpStarted = true;
            return true;
        }
        return false;
    }

    void Update()
    {
        bool grounded = groundCheck.IsGrounded;
        // Do jump detection in Update() loop because its less likely to miss inputs than FixedUpdate()
        timeInTheAir = (grounded) ? 0 : timeInTheAir + Time.deltaTime;
    }

    private Vector3 UpdateSpeed(Vector3 groundInput, Vector3 currentSpeed, bool grounded)
    {
        Vector3 newSpeed = currentSpeed;
        float y = newSpeed.y;
        newSpeed.y = 0;
        newSpeed = UpdateGroundSpeed(groundInput, newSpeed, grounded);
        newSpeed.y = UpdateSpeedY(y, grounded);
        return newSpeed;
    }

    private Vector3 UpdateGroundSpeed(Vector3 groundInput, Vector3 currentSpeed, bool grounded)
    {
        bool pressingDirection = groundInput.magnitude != 0;
        bool moving = currentSpeed.magnitude != 0;
        Vector3 newSpeed = currentSpeed;
        if (moving && !pressingDirection)
        {
            float friction = (grounded) ? groundFriction : airFriction;
            currentGroundSpeed -= friction * Time.fixedDeltaTime;
        }
        else if (pressingDirection)
        {
            currentGroundSpeed += groundAcceleration * Time.fixedDeltaTime;
        }
        currentGroundSpeed = Mathf.Clamp(currentGroundSpeed, 0, maxGroundSpeed);
        Vector3 movementDirection = groundInput;
        if (movementDirection.magnitude == 0)
        {
            movementDirection = currentSpeed.normalized;
        }
        newSpeed = movementDirection * currentGroundSpeed;
        return newSpeed;
    }

    private float UpdateSpeedY(float y, bool grounded)
    {
        float newY = y;
        newY = ApplyGravity(newY, grounded);
        newY = ProcessJumpInput(newY, grounded);
        newY = ClampVerticalSpeed(newY, grounded);
        wasGrounded = grounded;
        return newY;
    }

    private float ProcessJumpInput(float y, bool grounded)
    {
        // Cut the jump the moment the player stops holding the button
        float newY = y;
        if (!holdingJump)
        {
            cutJumpShort = true;
        }
        if (grounded)
        {
            jumping = false;
        }
        if (jumpStarted)
        {
            newY = jumpImpulse;
            timeSinceJumpStarted = 0;
            jumpStarted = false;
            grounded = false;
            jumping = true;
        }
        return newY;
    }

    private float ClampVerticalSpeed(float y, bool grounded)
    {
        float newY = y;
        if (!grounded && jumping)
        {
            // If cutting the jump, lower the vertical speed
            if (cutJumpShort && timeSinceJumpStarted > minJumpTime)
            {
                newY = Mathf.Min(newY, cutJumpSpeedLimit);
            }
            timeSinceJumpStarted += Time.fixedDeltaTime;
        }
        newY = Mathf.Max(newY, -maxFallingSpeed);
        return newY;
    }

    private float ApplyGravity(float y, bool grounded)
    {
        float newY = y;
        float gravityValue = -gravity * Time.fixedDeltaTime;
        if (!grounded)
        {
            if (wasGrounded && newY < 0)
            {
                // Reset vertical speed the moment the player stops touching the ground (if it's not jumping)
                newY = 0;
            }
            else if (newY >= 0)
            {
                // Lower the gravity value if the character is ascending after a jump
                gravityValue = gravityValue * gravityAscensionModifier;
            }
        }
        newY += gravityValue;
        return newY;
    }

    private void UpdatePosition(Vector3 speed)
    {
        Quaternion rotation = Camera.main.transform.rotation;
        rotation = Quaternion.Euler(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
        characterController.Move(rotation * speed);
    }

    public void SetSpeed(float x, float y, float z)
    {
        currentSpeed.Set(x, y, z);
    }
}
