using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class FirstPersonCameraController : MonoBehaviour
{
    public static FirstPersonCameraController Instance;
    
    private float _xRot, _yRot;
    [SerializeField] private float Sensitivity = 1;
    private bool isHuman = true;
    [SerializeField] private Transform computerScreenViewPoint;
    [SerializeField] private Transform cameraHome;

    private void Awake()
    {
        Instance = this;
    }

    private Vector3 TransformReturn;

    [Range(0, 1)] public float i;
    // Start is called before the first frame update
    void Start()
    {
        // isHuman = false;
        // TransformReturn = transform.position;
        
        _xRot = 0;
        _yRot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(TransformReturn, computerScreenViewPoint.position, i);
        
        if (isHuman)
        {
            _xRot = Mathf.Clamp(_xRot, -80, 80);
            transform.localEulerAngles = new Vector3(-_xRot, 0, 0);
            transform.parent.localEulerAngles = new Vector3(0, _yRot, 0);
        }
    }

    public void HandleMouseInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _yRot += input.x * Sensitivity * Time.deltaTime;
        _xRot += input.y * Sensitivity * Time.deltaTime;
    }

    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isHuman)
            {
                StartCoroutine(moveToComputer());
                // GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Computer");
                // pointToReturnTo = transform;
                // transform.position = computerScreenViewPoint.position;
                // transform.eulerAngles = computerScreenViewPoint.eulerAngles;
                // print(transform.eulerAngles);
                // print(computerScreenViewPoint.eulerAngles);
            }
            else
            {
                float timeMoving = 0;
                while (timeMoving < 2) 
                {
                    Mathf.Lerp(computerScreenViewPoint.position.x,cameraHome.position.x, timeMoving / 2); 
                    timeMoving += Time.deltaTime;
                }
                GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Human");
                isHuman = true;
            }
        }
    }

    IEnumerator moveToComputer()
    {
        isHuman = false;
        Vector3 startingPosition = transform.position;
        Vector3 startingRotation = transform.eulerAngles;
        print("awh shit here we go again");
        GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Computer");
        float timeMoving = 0;
        float timeToMove = 2;
        while(timeMoving < timeToMove)
        {
            float T = timeMoving / timeToMove;
            // print($"A {T}  {Vector3.Lerp(positionToReturnTo, computerScreenViewPoint.position, T)}");
            // print($"B {Vector3.Lerp(positionToReturnTo, computerScreenViewPoint.position, 0.85f)}");
            // print($"C {positionToReturnTo} {transform.position}");
            transform.position = Vector3.Lerp(startingPosition, computerScreenViewPoint.position, T);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(startingRotation.x, computerScreenViewPoint.eulerAngles.x, T),
                Mathf.LerpAngle(startingRotation.y, computerScreenViewPoint.eulerAngles.y, T),
            Mathf.LerpAngle(startingRotation.z, computerScreenViewPoint.eulerAngles.z, T));
            timeMoving += Time.deltaTime;
            
            if (isHuman) yield break;
            yield return null;
            if (isHuman) yield break;
        }

        transform.position = computerScreenViewPoint.position;
        transform.eulerAngles = computerScreenViewPoint.eulerAngles;
        yield return null;
        print("done");
    }

    public IEnumerator returnToBody()
    {
        isHuman = true;
        float timeMoving = 0;
        float timeToMove = 2;
        GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Human");
        print($"wait for it {cameraHome.position} {transform.position}");
        while(timeMoving < timeToMove)
        {
            transform.position = Vector3.Lerp(computerScreenViewPoint.position, cameraHome.position, timeMoving / timeToMove);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(computerScreenViewPoint.eulerAngles.x, cameraHome.eulerAngles.x, timeMoving / timeToMove),
                Mathf.LerpAngle(computerScreenViewPoint.eulerAngles.y, cameraHome.eulerAngles.y, timeMoving / timeToMove),
                Mathf.LerpAngle(computerScreenViewPoint.eulerAngles.z, cameraHome.eulerAngles.z, timeMoving / timeToMove));
            timeMoving += Time.deltaTime;
            
            if(!isHuman) yield break;
            yield return null;
            if(!isHuman) yield break;
        }
        print($"not yet {cameraHome.position} {transform.position}");
        transform.position = cameraHome.position;
        transform.eulerAngles = cameraHome.eulerAngles;
        print($"done {cameraHome.position} {transform.position}");
    }
}
