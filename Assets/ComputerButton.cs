using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour
{
    public UnityEvent OnPress;

    public Boolean isTaskbarTab = false;

    public void OpenWindow(GameObject NewWindow)
    {
        NewWindow.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
