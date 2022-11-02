using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour
{
    public UnityEvent OnPress;
    [SerializeField] private GameObject otherButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColour()
    {
        otherButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
