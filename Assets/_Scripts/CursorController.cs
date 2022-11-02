using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;
    private RectTransform _transform;
    [SerializeField] private PlayerInput input;

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
        print(input.currentActionMap);
    }

    public void HandleCursorInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 delta = context.ReadValue<Vector2>();
            delta *= 0.01f;
            
            
            float x = Mathf.Clamp(_transform.anchoredPosition.x + delta.x, 0, 40);
            float y = Mathf.Clamp(_transform.anchoredPosition.y + delta.y, -30, 0);

            print($"delta.x: {delta.x} delta.y: {delta.y}\npos.x+delta.x: {_transform.anchoredPosition.x + delta.x} pos.y+delta.y:{_transform.anchoredPosition.x + delta.x}\nx: {x} y: {y}");
            
            _transform.anchoredPosition = new Vector3(x, y);
        }
    }

    public void HandleClickInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            print("click");
        }
    }

    public void test()
    {
        print("test");
    }
}
