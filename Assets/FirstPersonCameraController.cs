using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCameraController : MonoBehaviour
{
    private float _xRot, _yRot;
    [SerializeField] private float Sensitivity = 1;
    private bool isHuman = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _xRot = 0;
        _yRot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _xRot = Mathf.Clamp(_xRot, -80, 80);
        transform.localEulerAngles = new Vector3(-_xRot, 0, 0);
        transform.parent.localEulerAngles = new Vector3(0, _yRot, 0);
    }

    public void HandleMouseInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _yRot += input.x * Sensitivity * Time.deltaTime;
        _xRot += input.y * Sensitivity * Time.deltaTime;
        print($"{_xRot} {_yRot}");
    }

    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isHuman)
            {
                GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Computer");
                isHuman = false;
            }
            else
            {
                GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Human");
                isHuman = true;
            }
        }
    }
}
