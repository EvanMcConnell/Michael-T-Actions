using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatfromerCameraController : MonoBehaviour
{
    public float xRot;
    private float yRot;
    private float cameraUpDownSpeed = 5f;

    [SerializeField] Transform FollowPos;
    private void Start()
    {
        xRot = 0;
    }
    void FixedUpdate()
    {

        transform.position = FollowPos.position;

        // Camera Up and Down
        transform.localRotation = Quaternion.Euler(xRot*Time.deltaTime, yRot* Time.deltaTime, 0f);

        //TODO:
        //add limits axis to rot;
        //add zoom in and out with scroll;
    }

    public void HandleRotationInput(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        xRot += inputMovement.y;
        yRot += inputMovement.x;

    }

    //TODO ZOOM
}
