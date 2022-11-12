using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMetaData : MonoBehaviour
{
    [SerializeField] int wealth = 1;
    [SerializeField] Currency currency = Currency.kubaKoin;

    internal void PickupCoin()
    {
        GameManager.Instance.IncrementCurrency(currency, wealth);
        Destroy(gameObject);
    }
}
