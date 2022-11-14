using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldWideWeb : MonoBehaviour
{
    private int Debt = 0;

    [SerializeField] private TMPro.TextMeshProUGUI debtText, bankBalanceText, cashForCanhaBucks, convertedCanhaBucks, canhaBalanceText;
    
    GameManager gm => GameManager.Instance;

    private void Start()
    {
        debtText.text = Debt.ToString();
        bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
        canhaBalanceText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
        cashForCanhaBucks.text = "0";
        convertedCanhaBucks.text = "0";
    }

    public void GetLoan(int DebtIncurred)
    {
        gm.IncrementCurrency(Currency.realMoney, DebtIncurred);
        Debt += DebtIncurred;
        debtText.text = "-" + Debt;
        bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
    }

    public void BuyCanhaBucks()
    {
        int buckToBuy = int.Parse(convertedCanhaBucks.text);


        if (gm.GetCurrency(Currency.realMoney) < buckToBuy * 10 || gm.GetCurrency(Currency.realMoney) == 0)
        {
            print("TEST A: " + (gm.GetCurrency(Currency.realMoney) > buckToBuy * 10));
            print("TEST B: " + (gm.GetCurrency(Currency.realMoney) == 0));
            canhaBalanceText.text = "POOR";
            return;
        }
        
        
        convertedCanhaBucks.text = "0";
        cashForCanhaBucks.text = "0";
            
        gm.IncrementCurrency(Currency.canhaBucks, buckToBuy); 
        canhaBalanceText.text = gm.GetCurrency(Currency.canhaBucks).ToString(); 
        gm.IncrementCurrency(Currency.realMoney, -buckToBuy * 10); 
        bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
    }

    public void incrementCashForCanhaBucks()
    {
        int cash = int.Parse(cashForCanhaBucks.text);

        cash += 100;

        cashForCanhaBucks.text = cash.ToString();
        convertedCanhaBucks.text = (cash / 10).ToString();
        canhaBalanceText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
    }

    public void decreaseCashForCanhaBucks()
    {
        int cash = int.Parse(cashForCanhaBucks.text);

        if (cash <= 0) return;

        cash -= 100;
        
        cashForCanhaBucks.text = cash.ToString();
        convertedCanhaBucks.text = (cash / 10).ToString();
        canhaBalanceText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
    }
}
