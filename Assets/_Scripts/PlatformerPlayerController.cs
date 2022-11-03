using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayerController : MonoBehaviour
{
    CharacterController characterController;

    [Header("Speed")]
    [SerializeField] private float defaultSpeed = 2f;
    [SerializeField] private float SprintMultiplier = 1.8f;
    [SerializeField] public float gravity = -9.81f;
    private float currentSpeed;

    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private float cameraLeftRightSpeed = 5f;


    private float xAxis;
    private float yAxis;
    private float yRot;

    private bool isSprinting = false;

    Vector3 movementVector;
    Vector3 velocityVector;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        yRot = 0;

    }

    float speedUpTime = 0;
    void FixedUpdate()
    {
        Debug.Log(currentSpeed);

        if (xAxis != 0 || yAxis !=0)
        {
            movementVector = transform.right * xAxis + transform.forward * yAxis;
        }


        characterController.Move(movementVector * currentSpeed * (isSprinting ? SprintMultiplier : 1) * Time.deltaTime);
        transform.Rotate(new Vector3(0, yRot * Time.deltaTime, 0) * cameraLeftRightSpeed);
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
        yRot = inputMovement.x;
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("context");
         //   m_Rigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }
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