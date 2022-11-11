using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWideWeb : MonoBehaviour
{
    private int Debt = 0;

    [SerializeField] private TMPro.TextMeshProUGUI debtText, balanceText, canhaBucksText;
    
    GameManager gm => GameManager.Instance;

    private void Start()
    {
        debtText.text = Debt.ToString();
        balanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
        canhaBucksText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
    }

    public void GetLoan(int DebtIncurred)
    {
        gm.IncrementCurrency(Currency.realMoney, DebtIncurred);
        Debt += DebtIncurred;
        debtText.text = "-" + Debt;
        balanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
    }

    public void BuyCanhaBucks(int buckToBuy)
    {
        if (gm.GetCurrency(Currency.realMoney) >= buckToBuy * 10)
        {
            gm.IncrementCurrency(Currency.canhaBucks, buckToBuy);
            canhaBucksText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
            gm.IncrementCurrency(Currency.realMoney, -buckToBuy * 10);
            balanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
        }
        else
        {
            canhaBucksText.text = "POOR";
        }
    }
}
