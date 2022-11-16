using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    void Update()
    {
        transform.LookAt(cameraTransform);
    }
}
