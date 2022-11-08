using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurchaseController : MonoBehaviour
{
    [SerializeField] GameObject PurchasePrompt;
    
    private bool inPurchaseArea;
    Collider purchaseAreaCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PurchaseArea"))
        { 
            PurchasePrompt.SetActive(true);
            inPurchaseArea = true;
            purchaseAreaCollider = other;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PurchaseArea"))
        {
            PurchasePrompt.SetActive(false);
            inPurchaseArea = false;
            purchaseAreaCollider = null;

        }
    }

    public void InteractedPressed()
    {
        if (inPurchaseArea && purchaseAreaCollider != null)
        {
            //Purchase Success
            if (purchaseAreaCollider.TryGetComponent(out PurchaseArea pA))
            {

                if (pA.TryPurchase())
                {
                    inPurchaseArea = false;
                    purchaseAreaCollider.GetComponent<Collider>().enabled = false;
                }
                else
                {
                    Debug.Log("Purchased Failed, not enough cash");
                }
            }
            else
            {
                Debug.Log("No component of Purchase Area");
            }
        }
        else
        {
            Debug.Log("Not in purchaseArea or not in collider area");
        }
    }
}