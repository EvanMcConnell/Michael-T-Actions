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
        if (!GameManager.Instance.IsSubActive(subscriptionID))
        {
            GameManager.Instance.SubScribe(subscriptionID);
        } else
        {
            PlatformerPlayerController.Instance.PlayErrorSound();
        }
    } 

    public void Unlock(SubscriptionID ID)
    {
        if (subscriptionID != ID)
            return;


        lockObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = true;
    }
}
