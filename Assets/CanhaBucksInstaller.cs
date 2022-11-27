using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanhaBucksInstaller : MonoBehaviour
{
    public static CanhaBucksInstaller Instance;

    private void Awake() => Instance = this;
}
