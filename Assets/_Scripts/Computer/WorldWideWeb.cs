using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WorldWideWeb : MonoBehaviour
{
    public static WorldWideWeb Instance;
    
    private int Debt = 0;

    [SerializeField] private TMPro.TextMeshProUGUI debtText, bankBalanceText, cashForCanhaBucks, convertedCanhaBucks, canhaBalanceText;
    [SerializeField] private GameObject pauseGameOverlay;
    
    GameManager gm => GameManager.Instance;

    private void Awake()
    {
        Instance = this;
    }

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
        GameManager.Instance.PurchaseWithCurrency(Currency.realMoney, -100, "LOAn");
        Debt += DebtIncurred;
        debtText.text = "-" + Debt;
        bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
        BankTransactionHistoryManager._Instance.UpdateTransactionHistory();
    }

    public void BuyCanhaBucks()
    {
        int buckToBuy = int.Parse(convertedCanhaBucks.text);


        if (gm.GetCurrency(Currency.realMoney) < buckToBuy * 10 || gm.GetCurrency(Currency.realMoney) == 0)
        {
            canhaBalanceText.text = "POOR";
            return;
        }
        
        
        convertedCanhaBucks.text = "0";
        cashForCanhaBucks.text = "0";
            
        //gm.IncrementCurrency(Currency.canhaBucks, buckToBuy);
        GameManager.Instance.PurchaseWithCurrency(Currency.canhaBucks, -buckToBuy, "Top Up");
        canhaBalanceText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
        GameManager.Instance.PurchaseWithCurrency(Currency.realMoney, buckToBuy * 10, "Canha Bucks");
        //gm.IncrementCurrency(Currency.realMoney, -buckToBuy * 10); 
        bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
        
        BankTransactionHistoryManager._Instance.UpdateTransactionHistory();
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

    public void StartGame()
    {
        if (PlatformerPlayerController.Instance == null)
        {
            SceneManager.LoadScene("3DGameWorld", LoadSceneMode.Additive);
            print("AYYOOO WE LOADIN SCENES IN THIS BITCH");
        }

        pauseGameOverlay.SetActive(false);
        CursorController.Instance.toggle();
        GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");

        try
        {
            freeCanhaFromTheirEternalMotionlessTorment();
        }
        catch
        {
            //Canha does not exist
        }
    }

    public void freeCanhaFromTheirEternalMotionlessTorment()
    {
        PlatformerPlayerController.Instance.isPaused = false;
    }

    public void pauseGame()
    {
        pauseGameOverlay.SetActive(true);
        CursorController.Instance.toggle();
        GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Computer");
    }
}
