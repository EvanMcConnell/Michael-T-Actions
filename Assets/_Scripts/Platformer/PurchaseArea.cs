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

    public bool TryPurchase()
    {
        if (GameManager.Instance.PurchaseWithCurrency(currency, cost, displayName))
        {
            purchaseEvent.Invoke();

            if (TryGetComponent(out CharacterMetaData data))
            {
                data.SetDialogue(false);
            }
            return true;
        } 
        else
        {
            if (TryGetComponent(out CharacterMetaData data))
            {
                data.SetDialogue(true);
            }
            return false;
        }
    }
}