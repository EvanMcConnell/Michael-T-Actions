using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ComputerWindow : MonoBehaviour
{
    [SerializeField] private GameObject TaskbarTab, Desktop;

    private void OnEnable()
    {
        foreach (ComputerWindow window in transform.parent.GetComponentsInChildren<ComputerWindow>())
        {
            if(window != this)
                window.MinimiseWindow();
        }
        
        OpenWindow();
    }


    public void OpenWindow()
    {
        TaskbarTab.SetActive(true);
        TaskbarTab.transform.GetChild(0).gameObject.SetActive(false);
        TaskbarTab.transform.GetChild(1).gameObject.SetActive(true);
        Desktop.SetActive(false);
    }
    
    public void CloseWindow()
    {
        TaskbarTab.SetActive(false);
        Desktop.SetActive(true);
        gameObject.SetActive(false);
    }

    public void MinimiseWindow()
    {
        TaskbarTab.transform.GetChild(0).gameObject.SetActive(true);
        TaskbarTab.transform.GetChild(1).gameObject.SetActive(false);
        Desktop.SetActive(true);
        gameObject.SetActive(false);
    }
}
