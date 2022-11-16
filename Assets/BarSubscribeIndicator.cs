using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSubscribeIndicator : MonoBehaviour
{
    [SerializeField] RectTransform barTransform;
    [SerializeField] SubscriptionID ID; 

    void FixedUpdate()
    {
        barTransform.localScale = new Vector3(1, (GameManager.Instance.GetBarSize(ID)), 1);
    }
}
