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

    [SerializeField] private float sensitivitY = 2f;
    [SerializeField] private float sensitivitX = 4f;

    [SerializeField] private float zoomSensitivity = 0.01f;
    private float zoomness = 0;

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
        xRot = Mathf.Clamp(xRot, -20, 77);
        // Camera Up and Down
        transform.localEulerAngles = new Vector3(xRot, yRot, 0f);
        
        //TODO:
        //add limits axis to rot;
        //add zoom in and out with scroll;
    }

    public void addRoationInput(float x, float y)
    {
        xRot += y * Time.deltaTime * sensitivitY;
        yRot += x * Time.deltaTime * sensitivitX;
    }

    // public void HandleRotationInput(InputAction.CallbackContext context)
    // {
    //     Vector2 inputMovement = context.ReadValue<Vector2>();
    //     xRot += inputMovement.y;
    //     yRot += inputMovement.x;
    //
    // }
    //
    
    public void HandleMouseScroll(float input)
    {
        yScroll = Mathf.Sign(input);
        Debug.Log(yScroll);
        
        zoomness = Mathf.Clamp(zoomness + zoomSensitivity * yScroll * Time.deltaTime, 0, 1);
        
        float y = Mathf.Lerp(4, 0, zoomness); 
        float z = Mathf.Lerp(-12, 0, zoomness);
        
        GetComponentInChildren<Camera>().transform.localPosition = new Vector3(0, y, z);
    }

    //TODO ZOOM
}
