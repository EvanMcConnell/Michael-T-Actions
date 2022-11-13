using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurchaseController : MonoBehaviour
{
    [SerializeField] GameObject PurchasePrompt;
    
    private bool inPurchaseArea;
    Collider purchaseAreaCollider;

    bool canvasOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractArea") )
        {
            //If Dialogue Box
            if (other.transform.TryGetComponent(out CharacterMetaData Cdata) && !canvasOccupied)
            {
                canvasOccupied = true;
                Cdata.ActivateDialogueBox();
            }

            //If Purchaseable
            if (other.transform.TryGetComponent(out PurchaseArea purchaseArea))
            {
                if (!purchaseArea.purchased)
                {
                    PurchasePrompt.SetActive(true);
                    inPurchaseArea = true;
                    purchaseAreaCollider = other;
                }
            }
        }

        if (other.CompareTag("Coin"))
        {
            if (other.transform.TryGetComponent(out CoinMetaData coin))
            {
                coin.PickupCoin();
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("PlayerCatcher"))
        {
            if (other.transform.TryGetComponent(out PlayerCatcher playerCatcher))
                transform.position = playerCatcher.ReSpawnPoint.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractArea"))
        {
            if (other.transform.TryGetComponent(out PurchaseArea Pdata))
            {
                PurchasePrompt.SetActive(false);
                inPurchaseArea = false;
                purchaseAreaCollider = null;
            }

            if (other.transform.TryGetComponent(out CharacterMetaData data))
            {
                canvasOccupied = false;
                data.DeactivateDialogueBox();
            }
        }
    }

    public void InteractedPressed()
    {
        if (inPurchaseArea && purchaseAreaCollider != null)
        {
            //try purchase
            if (purchaseAreaCollider.TryGetComponent(out PurchaseArea pA))
            {
                //Purchase Success
                if (pA.TryPurchase())
                {
                    inPurchaseArea = false;
                    purchaseAreaCollider = null;
                    PurchasePrompt.SetActive(false);
                    pA.purchased = true;


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