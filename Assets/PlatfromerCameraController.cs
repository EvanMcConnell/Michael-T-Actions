using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatfromerCameraController : MonoBehaviour
{
    private float xRot;
    private float cameraUpDownSpeed = 5f;


    private void Start()
    {
        xRot = 0;
    }
    void FixedUpdate()
    {
        // Camera Up and Down
        transform.localRotation = Quaternion.Euler(xRot*Time.deltaTime, 0f, 0f);

        //TODO:
        //add limits axis to rot;
        //add zoom in and out with scroll;
    }

    public void HandleRotationInput(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        print($"input {inputMovement}");
        xRot += inputMovement.y;
    }

    //TODO ZOOM
}
