using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayerController : MonoBehaviour
{
    CharacterController characterController;

    [Header("Speed amd Movement")]
    [SerializeField] private float defaultSpeed = 2f;
    [SerializeField] private float SprintMultiplier = 1.8f;
    private float currentSpeed;

    [Header("Gravity")]
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private Transform groundCheckPivot;
    [SerializeField] private LayerMask groundMask;

    //Ground Check 
    private float groundDistance = 0.4f;

    [Header("")]
    [SerializeField] private float cameraLeftRightSpeed = 5f;

    //Axis Input
    private float xAxis;
    private float yAxis;
    private float yRot;

    //Checks 
    private bool isSprinting = false;
    private bool isGrounded = false;
    private bool isJumping = false;

    // Vector 3
    Vector3 movementVector;
    Vector3 velocityVector;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        yRot = 0;

    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheckPivot.position, groundDistance, groundMask);
//        Debug.Log(isGrounded);
//        Debug.Log(velocityVector.y);

        velocityVector.y += gravity * Time.deltaTime;

        if (isGrounded && velocityVector.y < 0)
        {
            velocityVector.y = 0;
        }

        if (isGrounded && isJumping)
        {
            Debug.Log("jumpes");
            velocityVector.y = Mathf.Sqrt(jumpHeight * -1 *  gravity);
        }

        if ((xAxis != 0 || yAxis !=0) && isGrounded)
        {
            movementVector = transform.right * xAxis + transform.forward * yAxis;
        }

        characterController.Move(velocityVector + ( movementVector * currentSpeed * (isSprinting ? SprintMultiplier : 1) * Time.deltaTime));
        
        transform.localRotation = Quaternion.Euler(0,yRot * Time.deltaTime, 0f);
    }

    IEnumerator ChangeCurrentSpeedSmoothly(float start, float end, float steps, float timeStep)
    {
        currentSpeed = start;
        float t = 0;

        if (0 != end)
        {
            while (currentSpeed <= end)
            {
                t += steps;
                currentSpeed = Mathf.Lerp(start, end, t);
                yield return new WaitForSeconds(timeStep);
            }
        }
        else
        {
            while (currentSpeed > end)
            {
                t += steps;
                currentSpeed = Mathf.Lerp(start, end, t);
                yield return new WaitForSeconds(timeStep);
            }
        }

        currentSpeed = end;
    }

    private void WeWalkin(bool val)
    {
        StopAllCoroutines();

        if (val)
        {
            StartCoroutine(ChangeCurrentSpeedSmoothly(currentSpeed, defaultSpeed, .05f, .01f));
        }
        else
        {
            StartCoroutine(ChangeCurrentSpeedSmoothly(currentSpeed, 0, .05f, .01f));
        }

    }

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        xAxis = inputMovement.x;
        yAxis = inputMovement.y;

        if (context.started) WeWalkin(true);

        if (context.canceled) WeWalkin(false);

    }

    public void HandleRotationInput(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        yRot += inputMovement.x;
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started) isJumping = true;

        if (context.canceled) isJumping = false;
    }

    public void HandleSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
        }

        if (context.canceled) isSprinting = false;
    }
}