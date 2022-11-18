using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsriptionPurchase : MonoBehaviour
{
    [SerializeField] SubscriptionID subscriptionID;
    public void SubPurchase()
    {
        GameManager.Instance.SubScribe(subscriptionID);
    }
}
