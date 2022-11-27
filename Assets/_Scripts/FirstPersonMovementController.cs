using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovementController : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody rb;
    [SerializeField] private float walkSpeed = 0.1f;
    [SerializeField] private Transform cameraHome;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FirstPersonCameraController.Instance.movementLocked)
        {
            Transform camReference = cameraHome;
            camReference.eulerAngles = new Vector3(0, camReference.eulerAngles.y, camReference.eulerAngles.z);
            rb.velocity = (camReference.right * movement.x + camReference.forward * movement.y) * walkSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    
    

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}