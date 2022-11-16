using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    Transform PlayerTransform;
    void Start()
    {
        PlayerTransform = PlatformerPlayerController.Instance.transform;
    }

    void Update()
    {
        transform.LookAt(PlayerTransform);
    }
}
