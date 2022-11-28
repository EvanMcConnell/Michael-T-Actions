using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCameraController : MonoBehaviour
{
    public static FirstPersonCameraController Instance;
    
    private float _xRot, _yRot;
    [SerializeField] private float Sensitivity = 1;
    public bool isHuman = true;
    private bool theEndIsNigh = false;
    public bool movementLocked = false;
    [SerializeField] private Transform cameraHome, computerScreenViewPoint;
    [SerializeField] private GameObject ComputerScreen, ComputerPrompt, DoorPrompt, EndingCutscene;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _xRot = 0;
        _yRot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementLocked)
        {
            _xRot = Mathf.Clamp(_xRot, -80, 80);
            transform.localEulerAngles = new Vector3(-_xRot, 0, 0);
            transform.parent.localEulerAngles = new Vector3(0, _yRot, 0);
        }
    }

    public void HandleMouseInput(InputAction.CallbackContext context)
    {
        if (!movementLocked)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _yRot += input.x * Sensitivity * Time.deltaTime;
            _xRot += input.y * Sensitivity * Time.deltaTime;
        }
    }

    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (theEndIsNigh)
            {
                EndingCutscene.SetActive(true);
                return;
            }


            StartCoroutine(moveToComputer());
        }
    }

    IEnumerator moveToComputer()
    {
        isHuman = false;
        movementLocked = true;
        Vector3 startingPosition = transform.position;
        Vector3 startingRotation = transform.eulerAngles;
        //print("awh shit here we go again");
        GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Intro");
        float timeMoving = 0;
        float timeToMove = 1;
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


        if (!ComputerScreen.activeInHierarchy)
        {
            //print("we gotta turn on the compooter baws");
            ComputerScreen.SetActive(true);
        }
        else
        {
            //print("we got a compooter 'ere baws");
            GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Computer");
        }
        
        JacksRoomAudioManager.Instance.ON();
    }

    public IEnumerator returnToBody()
    {
        JacksRoomAudioManager.Instance.OFF();
        
        isHuman = true;
        Vector3 startingPosition = transform.position;
        Vector3 startingRotation = transform.eulerAngles;
        GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Human");
        float timeMoving = 0;
        float timeToMove = 1;
        print($"wait for it {cameraHome.position} {transform.position}");
        while(timeMoving < timeToMove)
        {
            float T = timeMoving / timeToMove;
            print($"A {T}  {Vector3.Lerp(startingPosition, cameraHome.position, T)}");
            print($"B {Vector3.Lerp(startingPosition, cameraHome.position, 0.85f)}");
            print($"C {cameraHome.position} {transform.position}");
            transform.position = Vector3.Lerp(startingPosition, cameraHome.position, T);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(startingRotation.x, cameraHome.eulerAngles.x, T),
                Mathf.LerpAngle(startingRotation.y, cameraHome.eulerAngles.y, T),
                Mathf.LerpAngle(startingRotation.z, cameraHome.eulerAngles.z, T));
            timeMoving += Time.deltaTime;
            
            if(!isHuman) yield break;
            yield return null;
            if(!isHuman) yield break;
        }

        movementLocked = false;
        print($"not yet {cameraHome.position} {transform.position}");
        transform.position = cameraHome.position;
        transform.eulerAngles = cameraHome.eulerAngles;
        print($"done {cameraHome.position} {transform.position}");
    }

    public void WakeUp()
    {
        EndingCutscene.SetActive(true);
        // StartCoroutine(returnToBody());
        // theEndIsNigh = true;
        // ComputerPrompt.SetActive(false);
        // DoorPrompt.SetActive(true);
    }
}
