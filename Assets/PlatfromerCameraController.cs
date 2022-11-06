using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatfromerCameraController : MonoBehaviour
{
    public static PlatfromerCameraController Instance;
    
    private float xRot;
    private float yRot;
    private float yScroll;

    private float cameraUpDownSpeed = 5f;

    [SerializeField] Transform FollowPos;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        xRot = 0;
    }
    void FixedUpdate()
    {

        transform.position = FollowPos.position;

        // Camera Up and Down
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(xRot * Time.deltaTime, -20, 75), yRot* Time.deltaTime, 0f);
        
        //TODO:
        //add limits axis to rot;
        //add zoom in and out with scroll;
    }

    public void addRoationInput(float x, float y)
    {
        xRot += y;
        yRot += x;
    }

    // public void HandleRotationInput(InputAction.CallbackContext context)
    // {
    //     Vector2 inputMovement = context.ReadValue<Vector2>();
    //     xRot += inputMovement.y;
    //     yRot += inputMovement.x;
    //
    // }
    //
    // public void HandleMouseScroll(InputAction.CallbackContext context)
    // {
    //     Vector2 inputMovement = context.ReadValue<Vector2>();
    //     yScroll = inputMovement.y;
    //     Debug.Log(yScroll);
    //     KeyValuePair<String, KeyValuePair<int, bool>> x;
    // }

    //TODO ZOOM
}
