using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMoney : MonoBehaviour
{
    [SerializeField] Currency currency;
    public void GiveMoneyCall()
    {
        GameManager.Instance.IncrementCurrency(currency, 1);
    }
}
