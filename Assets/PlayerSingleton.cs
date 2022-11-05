using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton Instance;

    private void Awake() => Instance = this;

    public Transform CamTransform, GroundCheckPivot;
    public CharacterController controller;
}
