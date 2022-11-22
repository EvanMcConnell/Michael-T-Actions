using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PurchaseArea : MonoBehaviour
{
    [SerializeField] public string displayName;
    [SerializeField] internal int cost;
    [SerializeField] internal Currency currency;
    [SerializeField] UnityEvent purchaseEvent;
    internal bool purchased = false;
    [SerializeField] internal bool infinite = false;

    [SerializeField] private AudioClip purchaseSound;

    public bool TryPurchase()
    {
        if (GameManager.Instance.PurchaseWithCurrency(currency, cost, displayName))
        {
            purchaseEvent.Invoke();

            if (TryGetComponent(out CharacterMetaData data))
            {
                data.SetDialogue(false);
            }

            if (TryGetComponent(out AudioSource purchaseAudio))
            {
                purchaseAudio.pitch = 1f;

                purchaseAudio.PlayOneShot(purchaseSound);
            }

            return true;
        } 
        else
        {
            if (TryGetComponent(out CharacterMetaData data))
            {
                data.SetDialogue(true);
            }

            if (TryGetComponent(out AudioSource purchaseAudio))
            {
                purchaseAudio.pitch = .5f;
                purchaseAudio.PlayOneShot(purchaseSound);
            }

            return false;
        }
    }
}