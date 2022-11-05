using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] Transform camTransform;
    [SerializeField] Animator animator;
    Transform cameraNormal;

    [Header("Speed amd Movement")]
    [SerializeField] private float defaultSpeed = 2f;
    [SerializeField] private float sprintSpeed = 40f;
    private float currentSpeed;

    [Header("Gravity")]
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] private float jumpStrength = .3f;
    [SerializeField] private Transform groundCheckPivot;
    [SerializeField] private LayerMask groundMask;

    [Header("Forgiveness")]
    [SerializeField] private float groundColliderCheckSize = 0.4f;
    [SerializeField] private float coyoteTime = 0.4f;
    float coyoteTimeLeft;


    //Axis Input
    private float xAxis;
    private float yAxis;

    //Checks 
    private bool isSprinting = false;
    private bool isGrounded = false;

    private bool isJumping = false;
    private bool isAirJump = false;
    private bool DidAirJump = false;


    // Vectors to effect movement
    Vector3 movementVector;
    Vector3 airMovementVector;
    Vector3 velocityVector;
    Vector3 inputVector;

    float actualSpeed;

    void Start()
    {
        GameObject emptyGO = new GameObject();
        cameraNormal = emptyGO.transform;
        characterController = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        GroundedCheck();
        GravityAndJumpCal();
        MovementCal();
        AnimatorController();
    }

    void AnimatorController()
    {
        if (isGrounded && (xAxis != 0 || yAxis != 0))
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Jump", false);
            animator.SetBool("Idle", false);
        }
        else if (isGrounded)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", true);
        }
    }

    /// <summary>
    /// All the effects of gravity and jump force in one place
    /// </summary>
    void GravityAndJumpCal()
    {
        velocityVector.y += gravity * Time.deltaTime;

        if (isGrounded && velocityVector.y < 0)
        {
            velocityVector.y = 0;
        }

        if (isJumping)
        {
            velocityVector.y = jumpStrength;
            isJumping = false;
        }

        if (isAirJump)
        {
            // half normal jump
            velocityVector.y = jumpStrength / 2;
            movementVector = inputVector;

            actualSpeed = isSprinting ? sprintSpeed : defaultSpeed;
            isAirJump = false;
        }
    }

    /// <summary>
    /// All the movement on the X and Z plain calculated here in one place
    /// </summary>
    void MovementCal()
    {
        //CameraDirection normalised
        cameraNormal.localEulerAngles = new Vector3(0,
            camTransform.localEulerAngles.y, camTransform.localEulerAngles.z);

        //Direction to move taking into account the player input and postion of the camera relative to the player

        inputVector = cameraNormal.forward * yAxis + cameraNormal.right * xAxis;

        // Ground Movement
        if (isGrounded)
        {
            movementVector = inputVector;
            actualSpeed = currentSpeed;
            characterController.Move(velocityVector + (movementVector * actualSpeed * Time.deltaTime));
        }
        // Air Movement
        else
        {
            airMovementVector = inputVector / 8;
           characterController.Move(velocityVector + (movementVector * actualSpeed * Time.deltaTime) + airMovementVector);
        }

        transform.LookAt(transform.position + inputVector.normalized);
    }


    /// <summary>
    /// To check if the player is on the ground
    /// + a poor implimentation of coyotetime
    /// </summary>
    public void GroundedCheck()
    {
        Physics.CheckSphere(groundCheckPivot.position, groundColliderCheckSize, groundMask);

        if (Physics.CheckSphere(groundCheckPivot.position, groundColliderCheckSize, groundMask))
        {
            isGrounded = true;
            coyoteTimeLeft = coyoteTime;
        }
        else
        {
            if (coyoteTimeLeft > 0)
                coyoteTimeLeft -= Time.deltaTime;

            else
                isGrounded = false;
        }
    }


    /// <summary>
    /// To control players walking speed, based on if any inputs and if sprinting
    /// </summary>
    private void WeWalkin()
    {
        StopAllCoroutines();
        //if input
        if (xAxis != 0 || yAxis != 0)
        {
            if (isSprinting)
            {
                StartCoroutine(ChangeSpeed(currentSpeed, sprintSpeed, .5f));
            }
            else
            {
                StartCoroutine(ChangeSpeed(currentSpeed, defaultSpeed, .5f));
            }
        }
        //No input
        else
        {
            currentSpeed = 0;
        }
    }

    /// <summary>
    /// To Change Speed smoothly, gracefully, like a swan
    /// </summary>
    /// <param name="v_start">speed to start with</param>
    /// <param name="v_end">speed to end with</param>
    /// <param name="duration">time to change speed</param>
    /// <returns></returns>
    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            //Dont change speed while in the air
            while (!isGrounded) { yield return null; }

            currentSpeed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        currentSpeed = v_end;
    }


    /// INPUT HANDLERS


    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        xAxis = inputMovement.x;
        yAxis = inputMovement.y;

        if (context.started) WeWalkin();

        if (context.canceled) WeWalkin();

    }



    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                DidAirJump = false;
                isJumping = true;
            }
            else if (!DidAirJump)
            {
                isAirJump = true;
                DidAirJump = true;
            }

        }

        if (context.canceled)
        {
            isJumping = false;
            isAirJump = false;
        }
    }

    public void HandleSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
            WeWalkin();
        }

        if (context.canceled)
        {
            isSprinting = false;
            WeWalkin();
        }
    }

    /// GIZMOS

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckPivot.position, groundColliderCheckSize);
    }
}