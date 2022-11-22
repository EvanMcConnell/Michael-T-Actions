using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSub : MonoBehaviour
{
    [SerializeField] SubscriptionID subscriptionID;
    public void UnlockSubCall()
    {
        GameManager.Instance.UnlockSub(subscriptionID);
    }
}
