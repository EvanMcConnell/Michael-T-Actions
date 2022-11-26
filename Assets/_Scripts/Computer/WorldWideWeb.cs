using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WorldWideWeb : MonoBehaviour
{
    public static WorldWideWeb Instance;
    
    private int Debt = 0;

    [SerializeField] private TMPro.TextMeshProUGUI debtText, bankBalanceText, canhaBalanceText, canhaScreenBankBalanceText;
    [SerializeField] private GameObject pauseGameOverlay;

    private float bankPendingTimer;
    private int bankPendingAmount;
    private float canhaPendingTimer;
    private int canhaPendingAmount;
    [SerializeField] private GameObject canhaScreenPoorText;
    
    GameManager gm => GameManager.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        debtText.text = Debt.ToString();
        bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
        canhaScreenBankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
        canhaBalanceText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
        
    }

    public void GetLoan(int DebtIncurred) => StartCoroutine(BankReceiptBuffer());

    IEnumerator BankReceiptBuffer()
    {
        bankPendingAmount += 100;
        bankPendingTimer += 0.35f;
        
        Debt += 100;
        debtText.text = "-" + Debt;

        yield return new WaitForSecondsRealtime(0.35f);
        bankPendingTimer -= 0.35f;
        bankPendingTimer = Mathf.Clamp(bankPendingTimer, 0, Mathf.Infinity);
        print(bankPendingTimer);

        if (bankPendingTimer == 0)
        {
            GameManager.Instance.PurchaseWithCurrency(Currency.realMoney, -bankPendingAmount, "LOAn");
            bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
            canhaScreenBankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
            BankTransactionHistoryManager._Instance.UpdateTransactionHistory();
            bankPendingAmount = 0;
        }
    }

    public void BuyCanhaBucks()
    {
        if (int.Parse(canhaScreenBankBalanceText.text) > 0)
        {
            StartCoroutine(CanhaBucksReceiptBuffer());
            canhaScreenPoorText.SetActive(false);
        }
        else
        {
            canhaScreenPoorText.SetActive(true);
        }
    }

    IEnumerator CanhaBucksReceiptBuffer()
    {
        canhaPendingAmount += 10;
        canhaPendingTimer += 0.35f;
        
        
        GameManager.Instance.PurchaseWithCurrency(Currency.canhaBucks, -10, "Top Up");
        canhaBalanceText.text = gm.GetCurrency(Currency.canhaBucks).ToString();
        
        canhaScreenBankBalanceText.text = (int.Parse(canhaScreenBankBalanceText.text)-100).ToString();
        
        yield return new WaitForSecondsRealtime(0.35f);
        canhaPendingTimer -= 0.35f;

        
        canhaPendingTimer = Mathf.Clamp(canhaPendingTimer, 0, Mathf.Infinity);
        if (canhaPendingTimer == 0)
        {
            GameManager.Instance.PurchaseWithCurrency(Currency.realMoney, canhaPendingAmount * 10, "Canha Bucks");
            bankBalanceText.text = gm.GetCurrency(Currency.realMoney).ToString();
            BankTransactionHistoryManager._Instance.UpdateTransactionHistory();
            canhaPendingAmount = 0;
        }
    }

    public void StartGame()
    {
        if (PlatformerPlayerController.Instance == null)
        {
            SceneManager.LoadScene("3DGameWorld", LoadSceneMode.Additive);
            print("AYYOOO WE LOADIN SCENES IN THIS BITCH");
        }

        pauseGameOverlay.SetActive(false);
        CursorController.Instance.toggle(false);
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
        CursorController.Instance.toggle(true);
        GameManager.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Computer");
    }
}
