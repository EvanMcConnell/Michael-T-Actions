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
        rb.velocity = new Vector3(movement.x, 0, movement.y) * walkSpeed;
    }

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
