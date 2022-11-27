using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerInputHandler : MonoBehaviour
{
    /*
     *     CAMERA INPUT
     */
    
    public void HandleRotationInput(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        PlatfromerCameraController.Instance.addRoationInput(inputMovement.x, inputMovement.y);
    }

    public void HandleMouseScroll(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        //yScroll = inputMovement.y;
        //Debug.Log(yScroll);
    }
    
    /*
     *     MOVEMENT INPUT
     */
    private PlatformerPlayerController movementController => PlatformerPlayerController.Instance;

    private PlayerPurchaseController purchaseController => PlatformerPlayerController.Instance.GetComponent<PlayerPurchaseController>();

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();

        movementController.addMovementInput(inputMovement.x, inputMovement.y);

        if (context.started) movementController.WeWalkin();

        if (context.canceled) movementController.WeWalkin();

    }



    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed) movementController.OnJumpPressed();

        if (context.canceled) movementController.OnJumpReleased();
    }

    public void HandleSprintInput(InputAction.CallbackContext context)
    {
        if (context.started) movementController.OnSprintPressed();

        if (context.canceled) movementController.OnSprintReleased();
    }

    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            purchaseController.InteractedPressed();
        }
    }

    public void HandlePauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementController.isPaused = true;

            try
            {
                WorldWideWeb.Instance.pauseGame();
            }
            catch
            {
                //where's jack
            }
        }
    }
}
