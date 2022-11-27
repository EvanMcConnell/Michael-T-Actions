using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanhaBucksInstaller : MonoBehaviour
{
    public static CanhaBucksInstaller Instance;
    [SerializeField] private GameObject bucksButton;
    public bool installCanha = false;


    private void OnEnable()
    {
        if(installCanha) bucksButton.SetActive(true);
    }

    private void Awake() => Instance = this;
}
