using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsriptionPurchase : MonoBehaviour
{
    [SerializeField] SubscriptionID subscriptionID;
    [SerializeField] GameObject lockObject;

    private void Start()
    {

        if (!GameManager.Instance.IsSubUnlocked(subscriptionID))
        {
            lockObject.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void SubPurchase()
    {
        GameManager.Instance.SubScribe(subscriptionID);
    }

    public void Unlock(SubscriptionID ID)
    {
        Debug.Log("I ran");
        if (subscriptionID != ID)
            return;

        Debug.Log("I ran further");

        lockObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = true;

    }
}
