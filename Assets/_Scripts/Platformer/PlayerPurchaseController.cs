using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPurchaseController : MonoBehaviour
{
    [SerializeField] GameObject PurchasePrompt;
    [SerializeField] AudioClip coinNoise;
    AudioSource coinAudioSource;

    private bool inPurchaseArea;
    Collider purchaseAreaCollider;

    bool canvasOccupied = false;

    private void Start()
    {
        coinAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractArea") )
        {
            //If Dialogue Box
            if (other.transform.TryGetComponent(out CharacterMetaData Cdata) && !canvasOccupied)
            {
                canvasOccupied = true;
                Cdata.ActivateDialogueBox();
                Cdata.SetDialogueDefault();
            }

            if (other.transform.TryGetComponent(out KnockKnockArea interactArea))
            {
                PurchasePrompt.SetActive(true);
                PurchasePrompt.GetComponent<TextMeshPro>().SetText("Press E to knock...");
                inPurchaseArea = true;
                purchaseAreaCollider = other;
                return;
            }

            //If Purchaseable
            if (other.transform.TryGetComponent(out PurchaseArea purchaseArea))
            {
                if (!purchaseArea.purchased)
                {
                    
                    PurchasePrompt.SetActive(true);
                    PurchasePrompt.GetComponent<TextMeshPro>().SetText($"{purchaseArea.displayName}\n{purchaseArea.cost} {purchaseArea.currency.ToString()}\ne to purchase");
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
                coinAudioSource.PlayOneShot(coinNoise);
            }
        }

        if (other.CompareTag("PlayerCatcher"))
        {
            if (other.transform.TryGetComponent(out PlayerCatcher playerCatcher))
            {
               StartCoroutine(TeleportSlowly(playerCatcher.ReSpawnPoint.position));
                
            }

        }

        if (other.CompareTag("Button"))
        {
            if (other.transform.TryGetComponent(out buttonController buttonController))
            {
                buttonController.ButtonDown();
            }
        }

        if (other.CompareTag("EventTrigger"))
        {
            if (other.transform.TryGetComponent(out EventTriggerArea eventTriggerArea))
            {
                eventTriggerArea.TriggerEvent();
            }
        }
    }

    

    IEnumerator TeleportSlowly(Vector3 point)
    {
        GetComponent<PlatformerPlayerController>().enabled = false;
        GetComponent<CharacterController>().Move(Vector3.zero);
        transform.position = point;

        yield return new WaitForSeconds(.02f);
        GetComponent<CharacterController>().Move(Vector3.zero);

        GetComponent<PlatformerPlayerController>().enabled = true;
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

        if (other.CompareTag("Button"))
        {
            if (other.transform.TryGetComponent(out buttonController buttonController))
            {
                buttonController.ButtonUp();
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

                    if (!pA.infinite)
                    {
                        pA.purchased = true;
                    }


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

        PopUpController popup = (PopUpController)FindObjectOfType(typeof(PopUpController));
        if (popup)
        {
            popup.InteractWithPopUp();
        }
    }
}