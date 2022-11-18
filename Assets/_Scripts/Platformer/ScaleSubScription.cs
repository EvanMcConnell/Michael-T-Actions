using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSubScription : MonoBehaviour
{
    [SerializeField] SubscriptionID subscriptionID;
    
    RectTransform rectTransform;
    void Start()
    {
        
    }

    void Update()
    {
        rectTransform.localScale = new Vector3(0, GameManager.Instance.GetSubTimeLeft(subscriptionID),0);
    }
}
