using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlatformerPlayerController : MonoBehaviour
{
    public static PlatformerPlayerController Instance;

    [HideInInspector]
    public bool isPaused = false;
    
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
    float jumpStrengthOriginal;
    [SerializeField] private Transform groundCheckPivot;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask movingPlatformMask;

    [Header("Forgiveness")]
    [SerializeField] private float groundColliderCheckSize = 0.4f;
    [SerializeField] private float coyoteTime = 0.4f;
    float coyoteTimeLeft;

    [Header("Effects")] 
    [SerializeField] private ParticleSystem landingEffect;
    [SerializeField] private ParticleSystem walkingEffect;

    [SerializeField] private CharacterSFX soundEffects;
    [SerializeField] private AudioSource audioSrc;

    //Axis Input
    private float xAxis;
    private float yAxis;

    //Checks 
    private bool isSprinting = false;
    private bool isGrounded = false;
    private bool isOnMover = false;

    private bool isJumping = false;
    private bool isAirJump = false;
    private bool DidAirJump = false;


    // Vectors to effect movement
    Vector3 movementVector;
    Vector3 airMovementVector;
    Vector3 velocityVector;
    Vector3 inputVector;

    float actualSpeed;
    
    List<ConstraintSource> emptyConstraintList = new();

    private void Awake()
    {
        jumpStrengthOriginal = jumpStrength;
        Instance = this;
    }

    void Start()
    {
        GameObject emptyGO = new GameObject();
        cameraNormal = emptyGO.transform;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if ((xAxis != 0 || yAxis != 0) && isGrounded)
        {
            print("dusty");
            walkingEffect.Play();
        }
        else
        {
            walkingEffect.Stop();
        }
        
        print($"{walkingEffect.isEmitting} {walkingEffect.isPlaying}");
    }

    void FixedUpdate()
    {
        if (isPaused) return;
        
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

        if (isAirJump && GameManager.Instance.IsSubActive(SubscriptionID.doubleJump))
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

        if (GameManager.Instance.IsSubActive(SubscriptionID.zAxis))
        {
            inputVector = cameraNormal.forward * yAxis + cameraNormal.right * xAxis;
        } else
        {
            inputVector = cameraNormal.right * xAxis;
        }
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
        //if(Physics.CheckSphere(groundCheckPivot.position, groundColliderCheckSize, movingPlatformMask));

        if (Physics.CheckSphere(groundCheckPivot.position, groundColliderCheckSize, groundMask))
        {
            if (isGrounded == false)
            {
                landingEffect.Play();
                audioSrc.PlayOneShot(soundEffects.Land);
            }
            
            isGrounded = true;
            coyoteTimeLeft = coyoteTime;
            
            if(Physics.Raycast(groundCheckPivot.position, -transform.up, out RaycastHit info, groundColliderCheckSize, movingPlatformMask))
            {
                isOnMover = true;
                characterController.Move(info.transform.GetComponent<Rigidbody>().velocity * Time.deltaTime);
            }
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
    public void WeWalkin()
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

    public void addMovementInput(float x, float y)
    {
        xAxis = x;
        yAxis = y;

        if (!GameManager.Instance.IsSubActive(SubscriptionID.zAxis) && (y > 0 || y < 0))
        {
            audioSrc.PlayOneShot(soundEffects.ErrorSound);
        }
    }
    
    public void OnJumpPressed()
    {
        if (isGrounded)
        {
            audioSrc.PlayOneShot(soundEffects.Jump);
            DidAirJump = false;
            isJumping = true;
        }
        else if (!DidAirJump)
        {
            audioSrc.PlayOneShot(soundEffects.AirJump);
            landingEffect.Play();
            isAirJump = true;
            DidAirJump = true;

            if (!GameManager.Instance.IsSubActive(SubscriptionID.doubleJump))
            {
                audioSrc.PlayOneShot(soundEffects.ErrorSound);
            }
        }
    }

    public void HigherJump(float strength) => velocityVector.y = strength;

    IEnumerator ResetJump(float strength)
    {
        
        audioSrc.PlayOneShot(soundEffects.Jump);
        DidAirJump = false;

        jumpStrength = strength;
        isJumping = true;

        yield return new WaitForSeconds(.1f);
        jumpStrength = jumpStrengthOriginal;
        yield return new WaitForSeconds(.1f);
        jumpStrength = jumpStrengthOriginal;

    }

    public void OnJumpReleased()
    {
        isJumping = false;
        isAirJump = false;
    }
    
    public void OnSprintPressed()
    {
        if (!GameManager.Instance.IsSubActive(SubscriptionID.sprint))
            audioSrc.PlayOneShot(soundEffects.ErrorSound);

        isSprinting = true;
        WeWalkin();
    }

    public void OnSprintReleased()
    {
        isSprinting = false;
        WeWalkin();
    }

    /// GIZMOS

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPivot.position, groundColliderCheckSize);
    }
}

[Serializable]
class CharacterSFX
{
    public AudioClip Jump;
    public AudioClip AirJump;
    public AudioClip Land;
    public AudioClip ErrorSound;

}