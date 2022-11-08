using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PurchaseArea : MonoBehaviour
{
    [SerializeField] private string displayName;
    [SerializeField] private int cost;
    [SerializeField] Currency currency;
    [SerializeField] UnityEvent purchaseEvent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool TryPurchase()
    {
        if (GameManager.Instance.PurchaseWithCurrency(currency, cost))
        {
            purchaseEvent.Invoke();
            return true;
        } 
        else
        {
            Debug.Log("POOR");
            return false;
        }
    }
}