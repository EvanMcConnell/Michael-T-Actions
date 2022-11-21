using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeScreenSpaceGameCamera : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().worldCamera = PlatfromerCameraController.Instance.GetComponentInChildren<Camera>();
    }
}
