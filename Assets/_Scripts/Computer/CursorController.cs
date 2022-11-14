using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;
    private RectTransform _transform;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    public void HandleCursorInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 delta = context.ReadValue<Vector2>();
            //delta *= 0.01f;
            
            float x = Mathf.Clamp(_transform.anchoredPosition.x + delta.x, 0, 640);
            float y = Mathf.Clamp(_transform.anchoredPosition.y + delta.y, 0, 480);
            
            _transform.anchoredPosition = new Vector3(x, y);
        }
    }

    public void HandleClickInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // print("click");
            foreach (ComputerButton button in transform.parent.GetComponentsInChildren<ComputerButton>())
            {
                RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
                Vector2 buttonPosition;
                if (!button.isTaskbarTab)
                {
                    buttonPosition = new Vector2(buttonRectTransform.anchoredPosition.x,
                        buttonRectTransform.anchoredPosition.y);
                }
                else
                {
                    RectTransform parentRectTransform = button.transform.parent.GetComponent<RectTransform>();
                    RectTransform grandParentRectTransform = button.transform.parent.parent.GetComponent<RectTransform>();
                    
                    buttonPosition = new Vector2(parentRectTransform.anchoredPosition.x + grandParentRectTransform.anchoredPosition.x,
                        parentRectTransform.anchoredPosition.y * -1 + grandParentRectTransform.anchoredPosition.y);
                }

                print($"{button.name} {buttonPosition} {button.transform.localPosition}");
                if (_transform.anchoredPosition.x < buttonPosition.x - (buttonRectTransform.rect.width/2)){
                    print("left: " + button.name);
                    continue;
                }
                if (_transform.anchoredPosition.x > buttonPosition.x + (buttonRectTransform.rect.width/2)){
                    print("right: " + button.name);
                    continue;
                }
                
                if (_transform.anchoredPosition.y < buttonPosition.y - (buttonRectTransform.rect.height/2))
                {
                    print("down: " + button.name);
                    continue;
                }
                
                if (_transform.anchoredPosition.y > buttonPosition.y + (buttonRectTransform.rect.height/2))
                {
                    print("up: " + button.name);
                    continue;
                }

                if (_transform.anchoredPosition.x < buttonPosition.x - (buttonRectTransform.rect.width/2) ||
                    _transform.anchoredPosition.x > buttonPosition.x + (buttonRectTransform.rect.width/2) ||
                    _transform.anchoredPosition.y < buttonPosition.y - (buttonRectTransform.rect.height/2) ||
                    _transform.anchoredPosition.y > buttonPosition.y + (buttonRectTransform.rect.height/2))
                {
                    continue;
                }
                
                print("clicking: " + button.name);
                button.OnPress.Invoke();
            }
        }
    }
    
    public void HandleReturnToBodyInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            print("returning to body");
            StartCoroutine(FirstPersonCameraController.Instance.returnToBody());
        }
    }
}
