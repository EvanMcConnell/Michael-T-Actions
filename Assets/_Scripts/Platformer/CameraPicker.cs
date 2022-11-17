using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPicker : MonoBehaviour
{
    [SerializeField] private Canvas[] canvases;
    
    void Start()
    {
        print("maybe");
        if (Jack.corporealForm != null)
        {
            print("nah");
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);


            foreach (Canvas canvas in canvases)
            {
                canvas.worldCamera = transform.GetChild(1).GetComponent<Camera>();
            }
        }
    }
}
