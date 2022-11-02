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
        print("started");
    }

    void Update()
    {
        
    }

    public void HandleCursorInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 delta = context.ReadValue<Vector2>();
            delta *= 0.01f;
            
            float x = Mathf.Clamp(_transform.anchoredPosition.x + delta.x, 0, 40);
            float y = Mathf.Clamp(_transform.anchoredPosition.y + delta.y, 0, 30);
            
            _transform.anchoredPosition = new Vector3(x, y);
        }
    }

    public void HandleClickInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            print("click");
            foreach (ComputerButton button in transform.parent.GetComponentsInChildren<ComputerButton>())
            {
                RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
                if (_transform.anchoredPosition.x < buttonRectTransform.anchoredPosition.x - (buttonRectTransform.rect.width/2) ||
                    _transform.anchoredPosition.x > buttonRectTransform.anchoredPosition.x + (buttonRectTransform.rect.width/2) ||
                    _transform.anchoredPosition.y < buttonRectTransform.anchoredPosition.y - (buttonRectTransform.rect.height/2) ||
                    _transform.anchoredPosition.y > buttonRectTransform.anchoredPosition.y + (buttonRectTransform.rect.height/2))
                {
                    break;
                }
                
                button.OnPress.Invoke();
            }
        }
    }

    public void test()
    {
        print("test");
    }
}
